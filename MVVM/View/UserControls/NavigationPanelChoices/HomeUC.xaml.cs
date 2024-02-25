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
using MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview;

namespace MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices
{
    /// <summary>
    /// Логика взаимодействия для HomeUC.xaml
    /// </summary>
    public partial class HomeUC : UserControl
    {
        public string guessedImage { get; set; }
        public HomeUC()
        {
            InitializeComponent();
            guessedImage = "D:\\C#projects\\MovieRecommendationsApp\\Datas\\Images\\logInImage.jpg";
            for (int i = 0; i < 13; i++)
            {
                MovieInfoPreview movieInfoPreview = new MovieInfoPreview(this);
                movieInfoPreview.Margin = new Thickness(5);
                movieInfoPreview.Height = 300;
                movieInfoPreview.Width = 300;
                moviesContainer.Children.Add(movieInfoPreview);
            }
            DataContext = this;
        }
    }
}
