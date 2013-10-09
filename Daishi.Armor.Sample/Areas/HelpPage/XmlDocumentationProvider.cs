#region Includes

using System;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Http.Controllers;
using System.Web.Http.Description;
using System.Xml.XPath;

#endregion

namespace Daishi.Armor.Sample.Areas.HelpPage {
    /// <summary>
    ///     A custom <see cref="IDocumentationProvider" /> that reads the API documentation from an XML documentation file.
    /// </summary>
    public class XmlDocumentationProvider : IDocumentationProvider {
        private readonly XPathNavigator _documentNavigator;
        private const string MethodExpression = "/doc/members/member[@name='M:{0}']";
        private const string ParameterExpression = "param[@name='{0}']";

        /// <summary>
        ///     Initializes a new instance of the <see cref="XmlDocumentationProvider" /> class.
        /// </summary>
        /// <param name="documentPath">The physical path to XML document.</param>
        public XmlDocumentationProvider(string documentPath) {
            if (documentPath == null) {
                throw new ArgumentNullException("documentPath");
            }
            var xpath = new XPathDocument(documentPath);
            _documentNavigator = xpath.CreateNavigator();
        }

        public virtual string GetDocumentation(HttpActionDescriptor actionDescriptor) {
            var methodNode = GetMethodNode(actionDescriptor);
            if (methodNode != null) {
                var summaryNode = methodNode.SelectSingleNode("summary");
                if (summaryNode != null) {
                    return summaryNode.Value.Trim();
                }
            }

            return null;
        }

        public virtual string GetDocumentation(HttpParameterDescriptor parameterDescriptor) {
            var reflectedParameterDescriptor = parameterDescriptor as ReflectedHttpParameterDescriptor;
            if (reflectedParameterDescriptor != null) {
                var methodNode = GetMethodNode(reflectedParameterDescriptor.ActionDescriptor);
                if (methodNode != null) {
                    var parameterName = reflectedParameterDescriptor.ParameterInfo.Name;
                    var parameterNode = methodNode.SelectSingleNode(String.Format(CultureInfo.InvariantCulture, ParameterExpression, parameterName));
                    if (parameterNode != null) {
                        return parameterNode.Value.Trim();
                    }
                }
            }

            return null;
        }

        private XPathNavigator GetMethodNode(HttpActionDescriptor actionDescriptor) {
            var reflectedActionDescriptor = actionDescriptor as ReflectedHttpActionDescriptor;
            if (reflectedActionDescriptor != null) {
                var selectExpression = String.Format(CultureInfo.InvariantCulture, MethodExpression, GetMemberName(reflectedActionDescriptor.MethodInfo));
                return _documentNavigator.SelectSingleNode(selectExpression);
            }

            return null;
        }

        private static string GetMemberName(MethodInfo method) {
            var name = String.Format(CultureInfo.InvariantCulture, "{0}.{1}", method.DeclaringType.FullName, method.Name);
            var parameters = method.GetParameters();
            if (parameters.Length != 0) {
                var parameterTypeNames = parameters.Select(param => GetTypeName(param.ParameterType)).ToArray();
                name += String.Format(CultureInfo.InvariantCulture, "({0})", String.Join(",", parameterTypeNames));
            }

            return name;
        }

        private static string GetTypeName(Type type) {
            if (type.IsGenericType) {
                // Format the generic type name to something like: Generic{System.Int32,System.String}
                var genericType = type.GetGenericTypeDefinition();
                var genericArguments = type.GetGenericArguments();
                var typeName = genericType.FullName;

                // Trim the generic parameter counts from the name
                typeName = typeName.Substring(0, typeName.IndexOf('`'));
                var argumentTypeNames = genericArguments.Select(t => GetTypeName(t)).ToArray();
                return String.Format(CultureInfo.InvariantCulture, "{0}{{{1}}}", typeName, String.Join(",", argumentTypeNames));
            }

            return type.FullName;
        }
    }
}