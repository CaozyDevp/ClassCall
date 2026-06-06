using ClassCall.Classroom.ViewModels;
using System.Windows;

namespace ClassCall.Classroom.Views
{
    public partial class NotifyWindow : Window
    {
        public NotifyWindow(string teacher, string subject, string content)
        {
            InitializeComponent();
            var viewModel = DataContext as NotifyViewModel;
            viewModel.CloseWindow += CloseWindow;
            viewModel.ShowMessage(subject, teacher, content);
        }

        private void CloseWindow()
        {
            Close();
        }
    }
}
