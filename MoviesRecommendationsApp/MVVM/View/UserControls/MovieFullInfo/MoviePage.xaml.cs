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
using TMDBApi;

namespace MoviesRecommendationsApp.MVVM.View.UserControls.MovieFullInfo
{
    /// <summary>
    /// Interaction logic for MoviePage.xaml
    /// </summary>
    public partial class MoviePage : UserControl
    {
        public MoviePage(Movie movie, MovieDetails details)
        {
            InitializeComponent();
            haha.Content = details.Budget;
        }
    }
}
