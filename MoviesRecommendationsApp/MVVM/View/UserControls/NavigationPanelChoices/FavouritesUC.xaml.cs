using MovieRecommendationsApp.MVVM.Model;
using MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMDBApi;

namespace MovieRecommendationsApp.MVVM.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для FavouritesUC.xaml
    /// </summary>
    public partial class FavouritesUC : UserControl, INotifyPropertyChanged
    {
        string login_;
        int favCapacity = 20;
        int favPtr = 0;
        List<int> favouritesIds;
        int _pagePtr = 0;
        public int PagePtr
        {
            get { return _pagePtr; }
            private set
            {
                _pagePtr = value; OnPropertyChanged("PagePtr");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public FavouritesUC(string login)
        {
            InitializeComponent();
            login_ = login;
            DBFunctions dBFunctions = new DBFunctions();
            favouritesIds = dBFunctions.GetUserPreferences(login_);
            FillFavourites(0);
            PagePtr = favPtr / favCapacity + 1;
            favPtr = favCapacity;
            DataContext = this;
        }

        void FillFavourites(int start)
        {
            if (favouritesIds.Count == 0) NoFavourites();
            if (start >= favouritesIds.Count) return;
            favouritesContainer.Children.Clear();
            foreach (int movieId in favouritesIds.Slice(start,
                Math.Min(favouritesIds.Count - start - 1, favCapacity)))
            {
                Movie movie = ApiQueriesProcessing.GetMovie(movieId).Result;
                MovieInfoPreview movieInfoPreview = new MovieInfoPreview(this, movie);
                movieInfoPreview.Margin = new Thickness(5);
                movieInfoPreview.Height = 320;
                movieInfoPreview.Width = 300;
                favouritesContainer.Children.Add(movieInfoPreview);
            }
        }

        private void ToNextPage(object sender, RoutedEventArgs e)
        {
            if (favPtr >= favouritesIds.Count) return;
            FillFavourites(favPtr);
            PagePtr = favPtr / favCapacity + 1;
            favPtr += favCapacity;
        }

        private void ToPrevPage(object sender, RoutedEventArgs e)
        {
            if (favPtr <= 0) return;
            favPtr -= favCapacity;
            FillFavourites(favPtr);
            PagePtr = favPtr / favCapacity + 1;
        }

        void NoFavourites()
        {
            favouritesContainer.Children.Clear();
            Label label = new Label();
            label.Content = "Пока в избранном нет добавленных фильмов";
            label.HorizontalAlignment = HorizontalAlignment.Center;
            label.SetResourceReference(Label.StyleProperty, "FlatLabel");
            label.FontSize = 40;
            favouritesContainer.Children.Add(label);
        }
    }
}
