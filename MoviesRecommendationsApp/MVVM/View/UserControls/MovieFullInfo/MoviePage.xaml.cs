using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
using TMDBApi;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace MoviesRecommendationsApp.MVVM.View.UserControls.MovieFullInfo
{
    /// <summary>
    /// Interaction logic for MoviePage.xaml
    /// </summary>
    public partial class MoviePage : UserControl
    {
        public ImageSource MovieImage { get; init; }
        public Movie Movie { get; init; }
        public MovieDetails MovieDetails { get; init; }
        public MoviePage(Movie movie, MovieDetails details)
        {
            InitializeComponent();
            MovieImage = new BitmapImage(movie.GetPosterUri());
            DataContext = this;
            Movie = movie;
            MovieDetails = details;
            FillGenres();
            FillCompanies();
        }
        void FillGenres()
        {
            Label label;
            foreach (int genreId in Movie.GenreIds)
            {
                label = new Label();
                label.Style = (Style)Application.Current.Resources["LightLabel"];
                label.FontSize = 25;
                label.Content = Movie.GetGenreById(genreId);
                movieGenres.Children.Add(label);
            }
        }
        void FillCompanies()
        {
            Label label;
            foreach (Company company in MovieDetails.ProductionCompanies)
            {
                label = new Label();
                label.Style = (Style)Application.Current.Resources["LightLabel"];
                label.FontSize = 25;
                label.Content = company.Name + ", " + company.OriginCountry;
                productionCompanies.Children.Add(label);
            }
        }
    }
}
