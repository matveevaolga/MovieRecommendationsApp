using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для RestorePassword.xaml
    /// </summary>
    public partial class RestorePassword : Window, INotifyPropertyChanged
    {
        string emailHint;
        public string EmailHint
        {
            get { return emailHint; }
            set
            { emailHint = value; OnPropertyChanged("EmailHint"); }
        }
        public RestorePassword()
        {
            InitializeComponent();
            EmailHint = "Введите почту";
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void OpenAuthorizationWindow(object sender, RoutedEventArgs e)
        {
            Authorization authWindow = new Authorization();
            authWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            authWindow.Show();
            this.Close();
        }

        private void SendInstruction(object sender, RoutedEventArgs e)
        {

        }

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            Registration regWindow = new Registration();
            regWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            regWindow.Show();
            this.Close();
        }
        private void ResetTextBox(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            EmailHint = "Введите почту";
            DataContext = this;
        }
    }
}
