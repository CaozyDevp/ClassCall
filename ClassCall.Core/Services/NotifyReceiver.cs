using ClassCall.Core.Messages;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ClassCall.Core.Constants;
using System.Collections.Generic;
using ClassCall.Core.Enums;

namespace ClassCall.Core.Services
{
    public class NotifyReceiver
    {
        private UdpClient _udpClient;
        private readonly int _port = NetConstants.NotifyPort;
        private readonly string _publicKeyXml;
        private readonly string _classroom;
        private bool _disposed;
        private readonly object _lock = new object();

        /// <summary>
        /// 显示消息的委托。参数列表：科目、教师、内容
        /// </summary>
        public Action<Subjects, string, string> ShowMessage { get; }
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="publicKeyXml">RSA公钥的Xml字符串</param>
        /// <param name="showMessage">显示消息的委托。参数依次为：科目、教师、内容</param>
        /// <param name="classroom">当前教室</param>
        /// <exception cref="ArgumentNullException"></exception>
        public NotifyReceiver(string publicKeyXml, Action<Subjects, string, string> showMessage, string classroom)
        {
            _publicKeyXml = publicKeyXml ?? throw new ArgumentNullException(nameof(publicKeyXml));
            ShowMessage = showMessage ?? throw new ArgumentNullException(nameof(showMessage));
            _classroom = classroom ?? throw new ArgumentNullException(nameof(classroom));
        }

        public async Task StartAsync()
        {
            lock (_lock)
            {
                if (_disposed) return;
                if (IsEnabled) return;
                if (_udpClient == null)
                {
                    _udpClient = new UdpClient(_port)
                    {
                        EnableBroadcast = true
                    };
                }
                IsEnabled = true;
            }
            while (IsEnabled)
            {
                try
                {
                    var result = await _udpClient.ReceiveAsync();

                    if (!NotifyMessage.TryParse(Encoding.UTF8.GetString(result.Buffer), out var notifyMessage))
                    {
                        continue;
                    }
                    if (notifyMessage == null)
                    {
                        continue;
                    }
                    if (!MessageSigner.VerifyMessage(notifyMessage, _publicKeyXml))
                    {
                        continue;
                    }
                    if (!IsDestination(notifyMessage.Destinations))
                    {
                        continue;
                    }
                    if (new DateTime(notifyMessage.TimeStamp) < DateTime.UtcNow - TimeSpan.FromMinutes(5))
                    {
                        var temp = new DateTime(notifyMessage.TimeStamp);
                        continue;
                    }

                    ShowMessage?.Invoke(notifyMessage.Subject, notifyMessage.Teacher, notifyMessage.Content);

                    var confirmation = new ConfirmMessage()
                    {
                        Classroom = _classroom,
                        ReceivedMessageId = notifyMessage.MessageId,
                        TimeStamp = DateTime.Now.Ticks
                    }.ToString();
                    var bytes = Encoding.UTF8.GetBytes(confirmation);

                    using (var client = new UdpClient())
                    {
                        await client.SendAsync(bytes, bytes.Length, result.RemoteEndPoint);
                    }
                }
                catch
                {
                    break;
                }
            }
            Dispose();
        }

        /// <summary>
        /// 判断当前消息是否是发给本教室的
        /// </summary>
        /// <param name="destinations">消息中包含的目标教室</param>
        private bool IsDestination(List<string> destinations)
        {
            foreach (var des in destinations)
            {
                if (des == _classroom)
                {
                    return true;
                }
            }
            return false;
        }

        public void Dispose()
        {
            lock (_lock)
            {
                if (_disposed) return;
                _disposed = true;
                IsEnabled = false;
                _udpClient?.Close();
                _udpClient = null;
            }
        }
    }
}
