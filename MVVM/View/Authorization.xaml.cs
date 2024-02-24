using MovieRecommendationsApp.MVVM.Model;
using MovieRecommendationsApp.MVVM.View;
using MovieRecommendationsApp.MVVM.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieRecommendationsApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Authorization : Window, INotifyPropertyChanged
    {
        string loginHint;
        public string LoginHint
        {
            get { return loginHint; }
            set
            { loginHint = value; OnPropertyChanged("LoginHint"); }
        }
        string passwordHint;
        public string PasswordHint
        {
            get { return passwordHint; }
            set
            { passwordHint = value; OnPropertyChanged("PasswordHint"); }
        }
        public Authorization()
        {
            InitializeComponent();
            LoginHint = "Введите логин";
            PasswordHint = "Введите пароль";
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void LogIn(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            if (login == "" | password == "")
            {
                if (login == "") LoginHint = "Вы не ввели логин";
                if (password == "") PasswordHint = "Вы не ввели пароль";
                return;
            }
            if (!DBHelpFunctional.LoginExists(login)) 
                { LoginHint = "Пользователя под введенным логином не существует"; return; }
            if (!DBHelpFunctional.PasswordCorrect(login, password)) 
                { PasswordHint = "Введен неверный пароль"; return; }
            OpenMainWindow(login);
        }

        private void ResetTextBox(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            switch (textBox.Name)
            {
                case "loginTextBox":
                    LoginHint = "Введите логин";
                    break;
                case "passwordTextBox":
                    PasswordHint = "Введите пароль";
                    break;
                default:
                    break;
            }
        }

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            Registration regWindow = new Registration();
            regWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            regWindow.Show();
            this.Close();
        }

        private void OpenMainWindow(string login)
        {
            MainWindow mainWindow = new MainWindow(login);
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void OpenRestorePasswordWindow(object sender, RoutedEventArgs e)
        {
            RestorePassword restorePassword = new RestorePassword();
            restorePassword.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            restorePassword.Show();
            this.Close();
        }
    }
}