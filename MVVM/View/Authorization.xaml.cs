using MovieRecommendationsApp.MVVM.View;
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
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void OpenRegistrationWindow(object sender, RoutedEventArgs e)
        {
            Registration regWindow = new Registration();
            regWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            regWindow.Show();
            this.Close();
        }
    }
}