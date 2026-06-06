using System;
using System.Windows;

namespace ClassCall.Teacher
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (!Configuration.LoadConfig())
            {
                Configuration.ResetConfig();
                Configuration.SaveConfig();
            }
            if (!Configuration.LoadKey())
            {
                MessageBox.Show("密钥加载失败，程序无法启动", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(0);
            }
        }
    }
}
