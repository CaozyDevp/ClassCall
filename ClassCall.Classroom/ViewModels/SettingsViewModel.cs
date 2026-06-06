using ClassCall.Mvvm;
using System;
using System.Windows.Input;

namespace ClassCall.Classroom.ViewModels
{
    internal class SettingsViewModel : ViewModelBase
    {
        /// <summary>
        /// 教室门牌号
        /// </summary>
        public string Classroom
        {
            get => _classroom;
            set => SetProperty(ref _classroom, value);
        }
        private string _classroom;

        /// <summary>
        /// 关闭当前窗体的事件
        /// </summary>
        public event Action CloseWindow;

        /// <summary>
        /// 向用户显示通知的事件
        /// </summary>
        public event Action<string> ShowNotify;

        public ICommand SaveCommand => new RelayCommand(_ =>
        {
            Configuration.Config.Classroom = Classroom;
            if (Configuration.SaveConfig())
            {
                CloseWindow?.Invoke();
            }
            else
            {
                ShowNotify?.Invoke("配置保存失败！");
            }
        });

        public void Init()
        {
            if (Configuration.Config == null)
            {
                return;
            }
            Classroom = Configuration.Config.Classroom;
        }
    }
}
