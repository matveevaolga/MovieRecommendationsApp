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

namespace MoviesRecommendationsApp.MVVM.View.UserControls.SelectMovies
{
    /// <summary>
    /// Interaction logic for SortingSelect.xaml
    /// </summary>
    public partial class SortingSelect : UserControl
    {
        string _param = "popularity";
        string _order = ".desc";
        public string GetSortingParametersString
        { get => string.Format("&sort_by={0}{1}", _param, _order); }

        public SortingSelect()
        {
            InitializeComponent();
            FillSortingOptions();
            FillSortingOrder();
        }

        void FillSortingOrder()
        {
            RadioButton radioButton;
            foreach (string order in ApiQueriesProcessing.SortingOrder.Keys)
            {
                radioButton = new RadioButton();
                radioButton.Margin = new Thickness(5);
                radioButton.Content = order;
                radioButton.FontSize = 25;
                radioButton.Click += new RoutedEventHandler(OrderChoice);
                sortingOrder.Children.Add(radioButton);
            }
        }

        void FillSortingOptions()
        {
            RadioButton radioButton;
            foreach (string param in ApiQueriesProcessing.SortingParameters.Keys)
            {
                radioButton = new RadioButton();
                radioButton.Margin = new Thickness(5);
                radioButton.Content = param;
                radioButton.FontSize = 25;
                radioButton.Click += new RoutedEventHandler(ParamChoice);
                sortingOptions.Children.Add(radioButton);
            }
        }

        void ParamChoice(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if ((bool)radioButton.IsChecked) _param 
                    = ApiQueriesProcessing.SortingParameters[radioButton.Content.ToString()];
            else _param = "popularity";
        }

        void OrderChoice(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = (RadioButton)sender;
            if ((bool)radioButton.IsChecked) _order
                    = ApiQueriesProcessing.SortingOrder[radioButton.Content.ToString()];
            else _order = ".desc";
        }
    }
}
