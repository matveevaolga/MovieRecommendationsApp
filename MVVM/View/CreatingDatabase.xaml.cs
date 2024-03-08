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
    /// Interaction logic for CreatingDatabase.xaml
    /// </summary>
    public partial class CreatingDatabase : Window
    {
        public CreatingDatabase()
        {
            InitializeComponent();
        }

        private void OpenAuthorizationWindow()
        {
            Authorization authWindow = new Authorization();
            authWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            this.Close();
            authWindow.Show();
        }

        private void CreateDataBase(object sender, RoutedEventArgs e)
        {
            string port = serverPort.Text.Trim();
            string username = serverUsername.Text.Trim();
            string password = serverPassword.Text.Trim();
            if (!int.TryParse(port, out int x) || username == "" || password == "")
            {
                errorMessage.Visibility = Visibility.Visible;
                return;
            }
            try
            {
                DBHelpFunctional.HelpCreateDataBase(port, username, password);
                OpenAuthorizationWindow();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                errorMessage.Visibility = Visibility.Visible;
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e) =>
            this.Close();

        private void MinimizeWindow(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;
    }
}
