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
using MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview;
using TMDBApi;

namespace MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices
{
    /// <summary>
    /// Логика взаимодействия для HomeUC.xaml
    /// </summary>
    public partial class HomeUC : UserControl, INotifyPropertyChanged
    {
        int _pagePtr = 1;
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

        public HomeUC()
        {
            InitializeComponent();
            DataContext = this;
        }

        public void FillMovieContainer()
        {
            PageWithMovies pageWithMovies = ApiQueriesProcessing.GetPageWithMovies(PagePtr).Result;
            moviesContainer.Children.Clear();
            foreach (Movie movie in pageWithMovies.Results)
            {
                MovieInfoPreview movieInfoPreview = new MovieInfoPreview(this, movie);
                movieInfoPreview.Margin = new Thickness(5);
                movieInfoPreview.Height = 320;
                movieInfoPreview.Width = 300;
                moviesContainer.Children.Add(movieInfoPreview);
            }
        }

        private void ToNextPage(object sender, RoutedEventArgs e)
        {
            try
            {
                PagePtr++;
                FillMovieContainer();
            }
            catch (IndexOutOfRangeException) { PagePtr--; }
            catch (AggregateException) { PagePtr--; }
            catch (Exception) { PagePtr--; }
        }

        private void ToPrevPage(object sender, RoutedEventArgs e)
        {
            if (PagePtr == 1) return;
            PagePtr--;
            FillMovieContainer();
        }
    }
}
