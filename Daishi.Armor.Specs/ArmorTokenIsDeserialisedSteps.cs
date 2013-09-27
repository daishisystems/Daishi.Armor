#region Includes

using System.Linq;
using System.Security.Claims;
using NUnit.Framework;
using TechTalk.SpecFlow;

#endregion

namespace Daishi.Armor.Specs {
    [Binding]
    public class ArmorTokenIsDeserialisedSteps {
        private string serialisedArmorToken;

        private ArmorToken armorToken;
        private ArmorToken deserialisedArmorToken;

        [Given(@"I have supplied a serialised ArmorToken")]
        public void GivenIHaveSuppliedASerialisedArmorToken() {
            armorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim")});
            var serialisor = new ArmorTokenSerialisor(armorToken);

            serialisor.Execute();
            serialisedArmorToken = serialisor.SerialisedArmorToken;
        }

        [When(@"I deserialise the ArmorToken")]
        public void WhenIDeserialiseTheArmorToken() {
            var deserialisor = new ArmorTokenDeserialisor(serialisedArmorToken);
            deserialisor.Execute();

            deserialisedArmorToken = deserialisor.DeserialisedArmorToken;
        }

        [Then(@"the result should match the original ArmorToken")]
        public void ThenTheResultShouldMatchTheOriginalArmorToken() {
            foreach (var expectedClaim in armorToken.Claims.Where(c => !c.Type.Equals("TimeStamp"))) {
                Claim claim;

                Assert.NotNull(claim = deserialisedArmorToken.Claims.SingleOrDefault(c => c.Type.Equals(expectedClaim.Type)));
                Assert.AreEqual(expectedClaim.Value, claim.Value);
            }
        }
    }
}