#region Includes

using System.Collections.Generic;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

#endregion

namespace Daishi.Armor.Sample {
    public class DummyDelegatingHandler : DelegatingHandler {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            IPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim> {
                new Claim("UserId", "paul.mooney@hmhco.com"),
                new Claim("Platform", "HMH")
            }));

            Thread.CurrentPrincipal = principal;
            HttpContext.Current.User = principal;

            return base.SendAsync(request, cancellationToken);
        }
    }
}