using MovieRecommendationsApp.MVVM.Model;
using MovieRecommendationsApp.MVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MovieRecommendationsApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string Login { get; }
        public MainWindow(string login)
        {
            InitializeComponent();
            Login = login;
        }

        private void NewLogInProcessing(object sender, RoutedEventArgs e)
        {
            DBHelpFunctional.UpdateDaysOnline(Login);
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e) =>
            this.Close();

        private void MinimizeWindow(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;
        
        private void ChangeWindowSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Template == (ControlTemplate)Application.Current.Resources["RestoreTemplate"])
            {
                WindowState = WindowState.Normal;
                button.Template = (ControlTemplate)Application.Current.Resources["MaximizeTemplate"];
            }
            else
            {
                WindowState = WindowState.Maximized;
                button.Template = (ControlTemplate)Application.Current.Resources["RestoreTemplate"];
            }
        }
    }
}
