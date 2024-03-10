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
using TMDBApi;

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
            FillMovieContainer();
            DataContext = this;
        }

        async void FillMovieContainer()
        {
            var page1 = ApiQueriesProcessing.GetPageWithMovies(1).Result;
            foreach (Movie movie in page1.Results)
            {
                MovieInfoPreview movieInfoPreview = new MovieInfoPreview(this, movie.Title,
                    movie.ReleaseDate);
                movieInfoPreview.Margin = new Thickness(5);
                movieInfoPreview.Height = 300;
                movieInfoPreview.Width = 300;
                moviesContainer.Children.Add(movieInfoPreview);
            }
        }
    }
}
