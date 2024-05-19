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
using MovieRecommendationsApp.MVVM.ViewModel;
using System.Resources;
using System.Reflection;
using MovieRecommendationsApp.MVVM.Model;

namespace MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview
{
    /// <summary>
    /// Логика взаимодействия для MovieInfoPreviewDetails.xaml
    /// </summary>
    public partial class MovieInfoPreviewDetails : UserControl
    {
        string userLogin_;
        int movieId_;
        public MovieInfoPreviewDetails(string overview,
            List<int> genreIds, double popularity,
            int movieId)
        {
            DataContext = this;
            InitializeComponent();
            movieOverview.Text = overview;
            moviePopularity.Content = Math.Round(popularity, 2);
            Label label;
            foreach (int genreId in genreIds) 
            {
                label = new Label();
                label.Style = (Style)Application.Current.Resources["DarkFlatLabel"];
                label.Content = Movie.GetGenreById(genreId);
                movieGenres.Children.Add(label);
            }
            userLogin_ = Application.Current.Resources["UserLogin"] as String;
            movieId_ = movieId;
            if (DBHelpFunctional.IsMovieInFavourites(userLogin_, movieId_))
            {
                favouritesButton.Style = Application.Current.Resources["FavouritesButtonAdded"] as Style;
            }
        }

        private void AddToFavourites(object sender, RoutedEventArgs e)
        {
            if (userLogin_ != "")
            {
                if (!DBHelpFunctional.IsMovieInFavourites(userLogin_, movieId_))
                {
                    DBHelpFunctional.HelpAddPreference(userLogin_, movieId_);
                    favouritesButton.Style = Application.Current.Resources["FavouritesButtonAdded"] as Style;
                }
                else
                {
                    DBHelpFunctional.HelpDeletePreference(userLogin_, movieId_);
                    favouritesButton.Style = Application.Current.Resources["FavouritesButtonNotAdded"] as Style;
                }
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, чтобы добавлять в избранное", "", MessageBoxButton.OK,
                    MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
    }
}
