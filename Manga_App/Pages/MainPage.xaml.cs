using Manga_App.ViewModels;

namespace Manga_App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();
        }        
    }
}
