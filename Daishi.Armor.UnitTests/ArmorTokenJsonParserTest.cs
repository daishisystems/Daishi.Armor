#region Includes

using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using NUnit.Framework;

#endregion

namespace Daishi.Armor.UnitTests {
    [TestFixture]
    internal class ArmorTokenJsonParserTest {
        [Test]
        public void ArmorTokenJsonParserParsesSerialisedArmorToken() {
            var expectedArmorToken = new ArmorToken("user.name@company.com", "myPlatform", 0, new[] {new Claim("Dummy", "Claim"),});

            var raw = Encoding.UTF8.GetBytes(Resources.SerialisedArmorToken);
            ArmorToken armorToken;

            using (Stream stream = new MemoryStream(raw)) {
                var parser = new ArmorTokenJsonParser(stream, "claims");
                Assert.DoesNotThrow(parser.Parse);

                armorToken = parser.Result.FirstOrDefault();
            }

            Assert.NotNull(armorToken);

            foreach (var expectedClaim in expectedArmorToken.Claims.Where(c => !c.Type.Equals("TimeStamp"))) {
                Claim claim;

                Assert.NotNull(claim = armorToken.Claims.SingleOrDefault(c => c.Type.Equals(expectedClaim.Type)));
                Assert.AreEqual(expectedClaim.Value, claim.Value);
            }
        }
    }
}