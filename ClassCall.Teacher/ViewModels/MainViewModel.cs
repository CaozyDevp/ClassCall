using System.Collections.Generic;
using System.Net;
using ClassCall.Mvvm;
using ClassCall.Core.Services;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using ClassCall.Core.Constants;
using ClassCall.Core.Enums;
using ClassCall.Teacher.Models;

namespace ClassCall.Teacher.ViewModels
{
    internal class MainViewModel : ViewModelBase
    {
        public string TeacherName
        {
            get => _teacherName;
            set => SetProperty(ref _teacherName, value);
        }
        private string _teacherName = string.Empty;

        public Subjects SelectedSubject
        {
            get => _selectedSubject;
            set => SetProperty(ref _selectedSubject, value);
        }
        private Subjects _selectedSubject;

        public SchoolGrades SelectedGrade
        {
            get => _selectedGrade;
            set => SetProperty(ref _selectedGrade, value);
        }
        private SchoolGrades _selectedGrade;

        public int SelectedClassroomIndex
        {
            get => _selectedClassroomIndex;
            set => SetProperty(ref _selectedClassroomIndex, value);
        }
        private int _selectedClassroomIndex = -1;

        public string Content
        {
            get => _content;
            set => SetProperty(ref _content, value);
        }
        private string _content = string.Empty;

        public string IpString
        {
            get => _ipString;
            set => SetProperty(ref _ipString, value);
        }
        private string _ipString = string.Empty;

        public List<string> Classrooms => (List<string>)SchoolConstants.Classrooms;

        public ICommand BroadcastCommand => new RelayCommand(async _ => await Broadcast());

        private async Task Broadcast()
        {
            var privateKey = Configuration.KeyManager.GetXmlString();
            if (privateKey == null || privateKey.Length == 0)
            {
                MessageBox.Show("没有配置私钥", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!CheckInput())
            {
                return;
            }

            try
            {
                if (SelectedSubject == default)
                {
                    MessageBox.Show("请选择正确的科目", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var classNum = SelectedClassroomIndex + 1;
                if (SelectedGrade == default)
                {
                    MessageBox.Show("请选择正确的年级", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var address = ClassroomMap.GetAddress(new Core.ClassInfo(SelectedGrade, classNum));

                using (var sender = new NotifySender(privateKey, TeacherName, SelectedSubject, IPAddress.Broadcast))
                {
                    var result = await sender.SendAsync(new List<string>() { address }, Content);
                    if (result != null && result.Count > 0 && result[0] == address)
                    {
                        MessageBox.Show("消息发送成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        MessageBox.Show("消息发送失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }

            }
            catch
            {
                MessageBox.Show("消息发送失败", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SaveConfig();
        }

        /// <summary>
        /// 根据配置文件初始化显示
        /// </summary>
        public void Init()
        {
            TeacherName = Configuration.Config.TeacherName;
            SelectedSubject = Configuration.Config.Subject;
        }

        private bool CheckInput()
        {
            if (string.IsNullOrEmpty(TeacherName.Trim()))
            {
                MessageBox.Show("请输入教师姓名", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (string.IsNullOrEmpty(Content.Trim()))
            {
                MessageBox.Show("请输入消息内容", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (SelectedSubject == default)
            {
                MessageBox.Show("请选择科目", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool SaveConfig()
        {
            Configuration.Config.TeacherName = TeacherName;
            Configuration.Config.Subject = SelectedSubject;
            return Configuration.SaveConfig();
        }
    }
}
