using ClassCall.Core.Enums;
using ClassCall.Core.Interfaces;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ClassCall.Core.Messages
{
    public class NotifyMessage : MessageBase<NotifyMessage>, ISignable
    {
        public Subjects Subject { get; set; }

        public string Teacher { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;

        public List<string> Destinations { get; set; } = new List<string>();

        public byte[] Signature { get; set; }

        public long TimeStamp { get; set; }

        public int MessageId { get; set; }

        public byte[] GetSignatureContent()
        {
            string integrated = $"{Subject}|{Teacher}|{Content}|{TimeStamp}|{MessageId}";
            foreach (var des in Destinations)
            {
                integrated += $"|{des}";
            }
            return SHA256.Create().ComputeHash(Encoding.UTF8.GetBytes(integrated));
        }
    }
}
