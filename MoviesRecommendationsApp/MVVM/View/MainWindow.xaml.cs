﻿using MovieRecommendationsApp.MVVM.Model;
using MovieRecommendationsApp.MVVM.ViewModel;
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
using System.Windows.Shapes;
using MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices;
using MovieRecommendationsApp.MVVM.View.UserControls;
using System.Security.Policy;
using MoviesRecommendationsApp.MVVM.View.UserControls.SelectMovies;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MovieRecommendationsApp.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public FiltersSelect filterPopup { get; set; }
        bool _openFilterPopup;
        public bool OpenFilterPopup
        {
            get => _openFilterPopup;
            set { _openFilterPopup = value; OnPropertyChanged("OpenFilterPopup"); }
        }

        public SortingSelect sortingPopup { get; set; }
        bool _openSortingPopup;
        public bool OpenSortingPopup
        {
            get => _openSortingPopup;
            set { _openSortingPopup = value; OnPropertyChanged("OpenSortingPopup"); }
        }

        public MainWindow(string login = "")
        {
            InitializeComponent();
            filterPopup = new FiltersSelect();
            sortingPopup = new SortingSelect();
            OpenFilterPopup = false;
            this.DataContext = this;
            Application.Current.Resources.Add("UserLogin", login);
            ReloadMovies();
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void NewLogInProcessing(object sender, RoutedEventArgs e)
        {
            string login = Application.Current.Resources["UserLogin"] as String;
            if (login != "")
                { DBHelpFunctional.UpdateDaysOnline(login); }
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void CloseWindow(object sender, RoutedEventArgs e) =>
            this.Close();

        private void MinimizeWindow(object sender, RoutedEventArgs e) =>
            WindowState = WindowState.Minimized;
        
        private void ChangeWindowSize(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            if (button.Template == (ControlTemplate)Application.Current.Resources["RestoreTemplate"])
            {
                WindowState = WindowState.Normal;
                button.Template = (ControlTemplate)Application.Current.Resources["MaximizeTemplate"];
            }
            else
            {
                WindowState = WindowState.Maximized;
                button.Template = (ControlTemplate)Application.Current.Resources["RestoreTemplate"];
            }
        }

        private void HomeChosen(object sender, RoutedEventArgs e) => ReloadMovies();
        private void AccountChosen(object sender, RoutedEventArgs e)
        {
            string login = Application.Current.Resources["UserLogin"] as String;
            if (login != "")
            {
                NavigationPanelChoice.Content = new AccountUC();
            }
            else 
            {
                MessageBox.Show("Авторизуйтесь, чтобы менять настройки профиля", "", MessageBoxButton.OK,
                    MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void FavouritesChosen(object sender, RoutedEventArgs e)
        {
            string login = Application.Current.Resources["UserLogin"] as String;
            if (login != "")
            {
                NavigationPanelChoice.Content = new FavouritesUC(login);
            }
            else
            {
                MessageBox.Show("Авторизуйтесь, чтобы добавлять в избранное", "", MessageBoxButton.OK,
                    MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.DefaultDesktopOnly);
            }
        }
        private void SortChosen(object sender, RoutedEventArgs e)
        {
            OpenSortingPopup = !OpenSortingPopup;
            if (OpenSortingPopup == false) ReloadMovies();
        }
        private void FilterChosen(object sender, RoutedEventArgs e)
        {
            OpenFilterPopup = !OpenFilterPopup;
            if (OpenFilterPopup == false) ReloadMovies ();
        }

        void ReloadMovies()
        {
            HomeUC homeUC = new HomeUC(filterPopup.GetFilterParametersString +
                                       sortingPopup.GetSortingParametersString);
            NavigationPanelChoice.Content = homeUC;
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
            var dark = new Uri(@"Dictionaries/DarkTheme.xaml", UriKind.Relative);
            var light = new Uri(@"Dictionaries/LightTheme.xaml", UriKind.Relative);

            if ((string)changingThemeButton.Tag == "Moon")
            {
                VisualStateManager.GoToState(changingThemeButton, "ToSun", false);
                changingThemeButton.Tag = "Sun";
                ChangeThemeDictionary(light, dark);
            }
            else
            {
                VisualStateManager.GoToState(changingThemeButton, "ToMoon", false);
                changingThemeButton.Tag = "Moon";
                ChangeThemeDictionary(dark, light);
            }
        }

        void ChangeThemeDictionary(Uri add, Uri remove)
        {
            ResourceDictionary? addDict = Application.LoadComponent(add) as ResourceDictionary;
            ResourceDictionary? removeDict = Application.LoadComponent(remove) as ResourceDictionary;
            Application.Current.Resources.MergedDictionaries.Remove(removeDict);
            Application.Current.Resources.MergedDictionaries.Add(addDict);
        }
    }
}
