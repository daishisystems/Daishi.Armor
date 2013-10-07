#region Includes

using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;

#endregion

namespace Daishi.Armor.Sample {
    public class ArmorDelegatingHandler : DelegatingHandler {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken) {
            return base.SendAsync(request, cancellationToken).ContinueWith(task => {
                var response = task.Result;

                var hashingKey = new byte[32];
                var encryptionKey = new byte[32];

                using (var provider = new RNGCryptoServiceProvider()) {
                    provider.GetBytes(hashingKey);
                    provider.GetBytes(encryptionKey);
                }

                var nonceGenerator = new NonceGenerator();
                nonceGenerator.Execute();

                var armorToken = new ArmorToken("paul.mooney@hmhco.com", "HMH", nonceGenerator.Nonce);

                var hash = new HashArmorTokenGenerationStep(new HMACSHA512HashingMechanismFactory(hashingKey), new EmptyArmorTokenGenerationStep());
                var encrypt = new EncryptArmorTokenGenerationStep(new RijndaelEncryptionMechanismFactory(encryptionKey), hash);
                var serialise = new SerialiseArmorTokenGenerationStep(new ArmorTokenSerialisor(), encrypt);

                var armorTokenGenerator = new ArmorTokenGenerator(armorToken, serialise);
                armorTokenGenerator.Execute();

                response.Headers.Add("ARMOR", armorTokenGenerator.ArmorToken);
                return response;
            });
        }
    }
}