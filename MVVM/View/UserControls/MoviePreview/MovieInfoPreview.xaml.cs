using MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview
{
    /// <summary>
    /// Логика взаимодействия для MovieInfoPreview.xaml
    /// </summary>
    public partial class MovieInfoPreview : UserControl
    {
        MovieInfoPreviewDetails details;
        HomeUC caller;
        public MovieInfoPreview(object caller)
        {
            details = new MovieInfoPreviewDetails();
            this.caller = (HomeUC)caller;
            InitializeComponent();
        }

        private void HideDetails(object sender, MouseEventArgs e)
        {
            //details.Margin = this.Margin;
            //details.Width = this.Width;
            //details.Height = this.Height;
            //details.Visibility = Visibility.Visible;
            //Console.WriteLine(details.Margin);

        }

        private void ShowDetails(object sender, MouseEventArgs e)
        {
            //details.Visibility = Visibility.Collapsed;
        }
    }
}
