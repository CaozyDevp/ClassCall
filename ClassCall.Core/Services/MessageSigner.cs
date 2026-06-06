using ClassCall.Core.Interfaces;
using System.Security.Cryptography;

namespace ClassCall.Core.Services
{
    public class MessageSigner
    {
        public static void SignMessage(ISignable message, string privateKeyXml)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXml);
                message.Signature = rsa.SignData(message.GetSignatureContent(), "SHA256");
            }
        }

        public static bool VerifyMessage(ISignable message, string publicKeyXml)
        {
            using (var rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKeyXml);
                return rsa.VerifyData(message.GetSignatureContent(), "SHA256", message.Signature);
            }
        }
    }
}
