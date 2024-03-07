using MovieRecommendationsApp.MVVM.View.UserControls.NavigationPanelChoices;
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

namespace MovieRecommendationsApp.MVVM.View.UserControls.MoviePreview
{
    /// <summary>
    /// Логика взаимодействия для MovieInfoPreview.xaml
    /// </summary>
    public partial class MovieInfoPreview : UserControl, INotifyPropertyChanged
    {
        MovieInfoPreviewDetails details;
        HomeUC caller;
        public MovieInfoPreviewDetails tipPopup { get; set; }
        bool openPopup;
        public bool OpenPopup
        {
            get { return openPopup; }
            set
            { openPopup = value; OnPropertyChanged("OpenPopup"); }
        }
        public MovieInfoPreview(object caller)
        {
            details = new MovieInfoPreviewDetails();
            this.caller = (HomeUC)caller;
            InitializeComponent();
            OpenPopup = false;
            tipPopup = new MovieInfoPreviewDetails();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void HideDetails(object sender, MouseEventArgs e)
        {
            if (!popupContent.IsFocused)
            {
                OpenPopup = false;
            }
        }

        private void ShowDetails(object sender, MouseEventArgs e)
        {
            OpenPopup = true;
        }
    }
}
