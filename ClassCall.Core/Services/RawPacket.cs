using System.Net;

namespace ClassCall.Core.Services
{
    public struct RawPacket
    {
        /// <summary>
        /// 接收到的数据
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public IPEndPoint Source { get; set; }

        public RawPacket(byte[] data, IPEndPoint source)
        {
            Data = data;
            Source = source;
        }
    }
}
