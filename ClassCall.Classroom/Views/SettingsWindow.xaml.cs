using ClassCall.Classroom.ViewModels;
using System.Windows;

namespace ClassCall.Classroom.Views
{
    public partial class SettingsWindow : Window
    {
        public SettingsWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitializeComponent();
            var viewModel = DataContext as SettingsViewModel;
            viewModel.CloseWindow += CloseWindow;
            viewModel.ShowNotify += ShowNotify;
            viewModel.Init();
        }

        private void ShowNotify(string text)
        {
            MessageBox.Show(text, "提示", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void CloseWindow()
        {
            DialogResult = true;
        }
    }
}
