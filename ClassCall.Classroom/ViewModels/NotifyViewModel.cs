using ClassCall.Classroom.Models;
using ClassCall.Mvvm;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace ClassCall.Classroom.ViewModels
{
    internal class NotifyViewModel : ViewModelBase
    {
        /// <summary>
        /// 显示的通知内容
        /// </summary>
        public string ContentText
        {
            get => _contentText;
            set => SetProperty(ref _contentText, value);
        }
        private string _contentText;

        /// <summary>
        /// 显示的通知来源
        /// </summary>
        public string SourceText
        {
            get => _sourceText;
            set => SetProperty(ref _sourceText, value);
        }
        private string _sourceText;

        /// <summary>
        /// 显示的消息距当前的时间
        /// </summary>
        public string TimeText
        {
            get
            {
                var offset = DateTime.Now - MessageTime;
                if (offset < TimeSpan.FromMinutes(1))
                {
                    return "刚刚";
                }
                else if (offset < TimeSpan.FromHours(1))
                {
                    return $"{offset.Minutes}分钟前";
                }
                else if (offset < TimeSpan.FromDays(1))
                {
                    return $"{offset.Hours}小时前";
                }
                else
                {
                    return $"{offset.Days}天前";
                }
            }
        }

        /// <summary>
        /// 消息到达的时间
        /// </summary>
        public DateTime MessageTime { get; set; }

        /// <summary>
        /// 关闭当前窗体的事件
        /// </summary>
        public event Action CloseWindow;

        public ICommand CloseCommand => new RelayCommand(_ => CloseWindow?.Invoke());

        public async void ShowMessage(string subject, string teacher, string content)
        {
            SourceText = $"{subject ?? ""} {teacher ?? "未知"}老师";
            ContentText = content;
            MessageTime = DateTime.Now;
            OnPropertyChanged(nameof(TimeText));
            await Task.Run(() =>
            {
                new Announcer(teacher, subject, content).Play();
            });
            var timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(10)
            };
            timer.Tick += UpdateTimeText;
            timer.Start();
        }

        private void UpdateTimeText(object sender, EventArgs e)
        {
            OnPropertyChanged(nameof(TimeText));
        }
    }
}
