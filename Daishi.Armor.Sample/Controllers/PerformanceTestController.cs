#region Includes

using System.Diagnostics;
using System.Security.Cryptography;
using System.Web.Http;

#endregion

namespace Daishi.Armor.Sample.Controllers {
    public class PerformanceTestController : ApiController {
        public string Get() {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            var hashingKey = new byte[32];
            var encryptionKey = new byte[32];

            using (var provider = new RNGCryptoServiceProvider()) {
                provider.GetBytes(hashingKey);
                provider.GetBytes(encryptionKey);
            }

            var nonceGenerator = new NonceGenerator();
            nonceGenerator.Execute();

            var armorToken = new ArmorToken("paul.mooney@hmhco.com", "HMH", nonceGenerator.Nonce);

            var hash = new HashArmorTokenGenerationStep(new HMACSHA256HashingMechanismFactory(hashingKey), new EmptyArmorTokenGenerationStep());
            var encrypt = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey), hash);
            var serialise = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor(), encrypt);

            var armorTokenGenerator = new ArmorTokenGenerator(armorToken, serialise);
            armorTokenGenerator.Execute();

            stopwatch.Stop();
            Trace.WriteLine(string.Concat("Time: ", stopwatch.ElapsedMilliseconds));

            return armorTokenGenerator.ArmorToken;
        }
    }
}