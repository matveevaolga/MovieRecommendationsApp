using MovieRecommendationsApp.MVVM.Model;
using MovieRecommendationsApp.MVVM.ViewModel;
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
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window, INotifyPropertyChanged
    {
        string loginHint;
        public string LoginHint
        {
            get { return loginHint; }
            set
            {
                loginHint = value; OnPropertyChanged("LoginHint");
            }
        }
        string passwordHint;
        public string PasswordHint
        {
            get { return passwordHint; }
            set
            {
                passwordHint = value; OnPropertyChanged("PasswordHint");
            }
        }
        string emailHint;
        public string EmailHint
        {
            get { return emailHint; }
            set
            {
                emailHint = value; OnPropertyChanged("EmailHint");
            }
        }
        public Registration()
        {
            InitializeComponent();
            LoginHint = "Введите логин";
            PasswordHint = "Введите пароль";
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

        private void SignUp(object sender, RoutedEventArgs e)
        {
            string login = loginTextBox.Text;
            string password = passwordTextBox.Text;
            string email = emailTextBox.Text;
            if (login == "" | password == "")
            {
                if (login == "") LoginHint = "Вы не ввели логин";
                if (password == "") PasswordHint = "Вы не ввели пароль";
                if (email == "") EmailHint = "Вы не ввели почту";
                return;
            }
            if (DBHelpFunctional.LoginExists(login))
            {
                LoginHint = "Пользователь с таким логином уже зарегистрирован"; return;
            }
            if (DBHelpFunctional.EmailExists(email))
            {
                EmailHint = "Пользователь с такой почтой уже зарегистрирован"; return;
            }
            try
            {
                EmailProcessing emailProcessing = new EmailProcessing(email);
                emailProcessing.SendWelcome(login, password);
            }
            catch
            {
                EmailHint = "Введенная почта некорректна"; return;
            }
            DBHelpFunctional.Register(login, password, email);
            toAuthorization.RaiseEvent(new RoutedEventArgs(Button.ClickEvent));
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
                case "emailTextBox":
                    EmailHint = "Введите почту";
                    break;
                default:
                    break;
            }
        }
    }
}
