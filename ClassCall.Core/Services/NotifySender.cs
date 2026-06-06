using ClassCall.Core.Constants;
using ClassCall.Core.Enums;
using ClassCall.Core.Messages;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClassCall.Core.Services
{
    public class NotifySender : IDisposable
    {
        private readonly UdpClient _udpClient = new UdpClient()
        {
            EnableBroadcast = true
        };
        private readonly int _port = NetConstants.NotifyPort;
        private readonly int _timeout = 2000;
        private readonly Random _random = new Random();
        private string _privateKeyXml;

        public string TeacherName { get; set; } = "";

        public Subjects Subject { get; set; } = Subjects.None;

        public IPAddress Address { get; set; }

        public NotifySender(string privateKey, string teacherName, Subjects subject, IPAddress address)
        {
            _privateKeyXml = privateKey;
            TeacherName = teacherName;
            Subject = subject;
            Address = address;
        }

        /// <summary>
        /// 发送通知消息，并等待确认消息
        /// </summary>
        /// <param name="destinations">要发送到的教室列表</param>
        /// <param name="content">消息内容</param>
        /// <returns>确认收到消息的教室列表</returns>
        public async Task<List<string>> SendAsync(List<string> destinations, string content)
        {
            if (_privateKeyXml == null || _privateKeyXml.Length == 0)
            {
                return new List<string>();
            }

            try
            {
                var message = new NotifyMessage
                {
                    Teacher = TeacherName,
                    Destinations = destinations,
                    Content = content,
                    Subject = Subject,
                    TimeStamp = DateTimeOffset.UtcNow.Ticks,
                    MessageId = _random.Next()
                };
                MessageSigner.SignMessage(message, _privateKeyXml);

                // 发送消息
                var target = new IPEndPoint(Address, _port);
                var data = Encoding.UTF8.GetBytes(message.ToString());
                await _udpClient.SendAsync(data, data.Length, target);

                // 接收消息
                var packets = await ReceiveRawPackets(_timeout);
                if (packets == null)
                {
                    return new List<string>();
                }
                // 处理消息
                List<string> received = new List<string>();

                foreach (var packet in packets)
                {
                    if (!ConfirmMessage.TryParse(Encoding.UTF8.GetString(packet.Data), out var confirmMessage))
                    {
                        continue;
                    }
                    if (confirmMessage == null ||
                        confirmMessage.ReceivedMessageId != message.MessageId ||
                        !IsClassroomIncluded(destinations, confirmMessage.Classroom))
                    {
                        continue;
                    }
                    received.Add(confirmMessage.Classroom);
                }

                return received;
            }
            catch
            {
                return new List<string>();
            }
        }

        /// <summary>
        /// 接收原始UDP数据包。如果在超时时间内一个都没有接收到，返回null
        /// </summary>
        /// <param name="timeout">超时时间，毫秒</param>
        /// <returns></returns>
        private async Task<List<RawPacket>> ReceiveRawPackets(int timeout)
        {
            try
            {
                var endTime = DateTime.UtcNow.AddMilliseconds(timeout);
                var rawPackets = new List<RawPacket>();
                while (DateTime.UtcNow < endTime)
                {
                    var remaining = (int)(endTime - DateTime.UtcNow).TotalMilliseconds;
                    if (remaining <= 0) break;
                    
                    var receiveTask = _udpClient.ReceiveAsync();
                    var timeoutTask = Task.Delay(timeout);

                    var completedTask = await Task.WhenAny(receiveTask, timeoutTask);
                    if (completedTask == timeoutTask)
                        return rawPackets;

                    var result = await receiveTask;
                    var packet = new RawPacket(result.Buffer, result.RemoteEndPoint);
                    rawPackets.Add(packet);
                }
                return rawPackets;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 判断目标教室是否包含在消息的目标列表中
        /// </summary>
        /// <param name="destinations">消息中包含的目标教室</param>
        /// <param name="classroom">要判断的教室</param>
        private bool IsClassroomIncluded(List<string> destinations, string classroom)
        {
            foreach (var des in destinations)
            {
                if (des == classroom)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            _udpClient?.Close();
        }
    }
}
