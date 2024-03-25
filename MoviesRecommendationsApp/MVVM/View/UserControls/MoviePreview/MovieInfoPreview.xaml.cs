using MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices;
using MoviesRecommendationsApp.MVVM.View.UserControls.MovieFullInfo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TMDBApi;

namespace MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview
{
    /// <summary>
    /// Логика взаимодействия для MovieInfoPreview.xaml
    /// </summary>
    public partial class MovieInfoPreview : UserControl, INotifyPropertyChanged
    {
        HomeUC caller;
        Movie movie;
        public MovieInfoPreviewDetails tipPopup { get; set; }
        bool openPopup;
        public bool OpenPopup
        {
            get { return openPopup; }
            set
            { openPopup = value; OnPropertyChanged("OpenPopup"); }
        }
        public BitmapImage MovieImage { get; init; }
        public MovieInfoPreview(object caller, Movie movie)
        {
            this.caller = (HomeUC)caller;
            InitializeComponent();
            OpenPopup = false;
            DataContext = this;
            movieTitle.Content = movie.Title;
            voteAverage.Content = Math.Round(movie.VoteAverage, 2);
            releaseDate.Content = movie.ReleaseDate;
            tipPopup = new MovieInfoPreviewDetails(movie.Overview,
                movie.GenreIds, movie.Popularity);
            MovieImage = new BitmapImage(movie.GetPosterUri());
            this.movie = movie;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void HideDetails(object sender, MouseEventArgs e)
        {
            if (!popupContent.IsMouseOver && !mainBorder.IsMouseOver)
                OpenPopup = false;
            else if (!popupContent.IsMouseOver)
                OpenPopup = false;
        }

        private void ShowDetails(object sender, MouseEventArgs e)
        {
            if (GetOpenPopups().Count > 0) return;
            Window activeWindow = Application.Current.Windows.
                OfType<Window>().SingleOrDefault(x => x.IsActive);
            Point mousePoint = Mouse.GetPosition(activeWindow);
            if (activeWindow != null && activeWindow.WindowState == WindowState.Maximized ||
                (mousePoint.Y + 200f <= Application.Current.MainWindow.ActualHeight &&
                 mousePoint.Y - 330f >= 0))
            {
                popupContent.Placement = PlacementMode.Right;
                if (mousePoint.X + 450f  > Application.Current.MainWindow.ActualWidth)
                {
                    popupContent.Placement = PlacementMode.Left;
                }
                OpenPopup = true;
            }
        }
        public static List<Popup> GetOpenPopups()
        {
            return PresentationSource.CurrentSources.OfType<HwndSource>()
                .Select(h => h.RootVisual)
                .OfType<FrameworkElement>()
                .Select(f => f.Parent)
                .OfType<Popup>()
                .Where(p => p.IsOpen).ToList();
        }

        private async void GoToMoviePage(object sender, RoutedEventArgs e)
        {
            // Асинхронные методы не должны всегда возвращать Task?
            MovieDetails details = await ApiQueriesProcessing.GetMovieDetails(movie.Id);
            caller.Content = new MoviePage(movie, details);
        }
    }
}
