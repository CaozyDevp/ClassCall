using ClassCall.Teacher.ViewModels;
using System.Windows;

namespace ClassCall.Teacher.Views
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            (DataContext as MainViewModel)?.Init();
        }
    }
}
