using System;
using System.Security.Cryptography;

namespace ClassCall.Core.Configs
{
    public class KeyManager : IDisposable
    {
        public RSACryptoServiceProvider RSAService { get; private set; }

        public bool IsPrivateKey { get; private set; }

        public string GetXmlString()
        {
            if (RSAService == null)
            {
                return null;
            }
            return RSAService.ToXmlString(IsPrivateKey);
        }

        public bool FromXmlString(string xml, bool isPrivate)
        {
            try
            {
                RSAService = new RSACryptoServiceProvider();
                RSAService.FromXmlString(xml);
                IsPrivateKey = isPrivate;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public void Dispose()
        {
            RSAService?.Dispose();
        }
    }
}
