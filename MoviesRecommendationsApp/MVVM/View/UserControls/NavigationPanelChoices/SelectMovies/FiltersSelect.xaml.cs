using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using static TMDBApi.Movie;

namespace MoviesRecommendationsApp.MVVM.View.UserControls.SelectMovies
{
    /// <summary>
    /// Interaction logic for FiltersSelect.xaml
    /// </summary>
    public partial class FiltersSelect : UserControl
    {
        List<int> GenresChosen = new List<int>();
        public string GetFilterParametersString
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("&with_genres=");
                sb.AppendJoin(",", GenresChosen);
                sb.Append($"&primary_release_year={releaseYear.Text}");
                return sb.ToString();
            }
        }

        public FiltersSelect()
        {
            InitializeComponent();
            FillGenres();
        }

        void FillGenres()
        {
            CheckBox checkBox;
            foreach (Movie.Genres genre in Enum.GetValues(typeof(Movie.Genres)))
            {
                checkBox = new CheckBox();
                checkBox.Margin = new Thickness(5);
                checkBox.Style = Application.Current.Resources["CheckBoxStyle"] as Style;
                checkBox.Content = genre.ToString();
                checkBox.Click += new RoutedEventHandler(ModifyGenresChosen);
                genresToChooseFrom.Children.Add(checkBox);
            }
        }

        void ModifyGenresChosen(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            int genre = (int)Enum.Parse(typeof(Movie.Genres), checkBox.Content.ToString());
            if (!(bool)checkBox.IsChecked) GenresChosen.Remove(genre);
            else GenresChosen.Add(genre);
        }
    }
}
