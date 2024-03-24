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
using TMDBApi;
using System.Windows.Shapes;

namespace MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview
{
    /// <summary>
    /// Логика взаимодействия для MovieInfoPreviewDetails.xaml
    /// </summary>
    public partial class MovieInfoPreviewDetails : UserControl
    {
        public MovieInfoPreviewDetails(string overview,
            List<int> genreIds, double popularity)
        {
            DataContext = this;
            InitializeComponent();
            movieOverview.Content = overview;
            moviePopularity.Content = Math.Round(popularity, 2);
            Label label;
            foreach (int genreId in genreIds) 
            {
                label = new Label();
                label.Style = (Style)Application.Current.Resources["FlatLabel"];
                label.Content = Movie.GetGenreById(genreId);
                movieGenres.Children.Add(label);
            }
        }
    }
}
