using ClassCall.Classroom.Views;
using ClassCall.Core.Enums;
using ClassCall.Core.Extensions;
using ClassCall.Core.Services;
using System;
using System.Windows;

namespace ClassCall.Classroom
{
    public partial class App : Application
    {
        private NotifyReceiver _notifyReceiver;

        private async void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Configuration.LoadConfig())
            {
                Configuration.ResetConfig();
                Configuration.Config.Classroom = new Random().Next().ToString();    // 生成随机教室号
                Configuration.SaveConfig();
            }
            if (!Configuration.LoadKey())
            {
                MessageBox.Show("密钥加载失败，程序无法启动", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }

            try
            {
                _notifyReceiver = new NotifyReceiver(Configuration.KeyManager.GetXmlString(), ShowMessage, Configuration.Config.Classroom);
                await _notifyReceiver.StartAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"响应器加载失败，程序无法启动\n错误信息：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }

        private void ShowMessage(Subjects subject, string teacher, string content)
        {
            var notifyWindow = new NotifyWindow(teacher, EnumExtension.GetDescription(subject), content);
            notifyWindow.Show();
        }
    }
}
