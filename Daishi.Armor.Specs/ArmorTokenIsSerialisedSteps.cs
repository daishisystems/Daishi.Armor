#region Includes

using System.Security.Claims;
using NUnit.Framework;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsSerialisedSteps {
        private ArmorToken armorToken;
        private ArmorTokenSerialisor serialisor;

        [Given(@"I have supplied an ArmorToken")]
        public void GivenIHaveSuppliedAnArmorToken() {
            armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
        }

        [When(@"I serialise the ArmorToken")]
        public void WhenISerialiseTheArmorToken() {
            serialisor = new ArmorTokenSerialisor(armorToken);
            serialisor.Execute();
        }

        [Then(@"ArmorTokenSerialisor creates a JSON representation of the ArmorToken")]
        public void ThenArmorTokenSerialisorCreatesAJSONRepresentationOfTheArmorToken() {
            Assert.DoesNotThrow(() => JsonConvert.DeserializeObject(serialisor.SerialisedArmorToken));
        }
    }
}