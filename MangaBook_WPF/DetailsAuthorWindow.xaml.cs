using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System.Windows;

namespace MangaBook_WPF
{
    public partial class AuthorWindow : Window
    {
        private readonly MangaDbContext _context;
        private readonly Author _author;

        public AuthorWindow(Author author, MangaDbContext context)
        {
            InitializeComponent();
            _author = author;
            _context = context;
            DataContext = author;
        }

        //sluit knop om de window te sluiten
        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //edit knop om de velden te bewerken van de auteur
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            brEditAuthorDetails.Visibility = Visibility.Visible;

            // Velden invullen met huidige gegevens
            tbEditName.Text = _author.Name;
            tbEditGeboorteDatum.Text = _author.geboorteDatum;
            tbEditDescription.Text = _author.description;
            tbEditFavoriteFood.Text = _author.favoriteFood;
            tbEditNationaliteit.Text = _author.Nationaliteit;
            tbEditFavorieteSport.Text = _author.FavorieteSport;
        }

        //opslaan nadat je het edit
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_author != null)
            {
                // Nieuwe waarden opslaan
                _author.Name = tbEditName.Text;
                _author.geboorteDatum = tbEditGeboorteDatum.Text;
                _author.description = tbEditDescription.Text;
                _author.favoriteFood = tbEditFavoriteFood.Text;
                _author.Nationaliteit = tbEditNationaliteit.Text;
                _author.FavorieteSport = tbEditFavorieteSport.Text;

                //Opslaan in database
                _context.Authors.Update(_author);
                _context.SaveChanges();

                // UI verversen
                brEditAuthorDetails.Visibility = Visibility.Collapsed;
                DataContext = null;
                DataContext = _author;

                MessageBox.Show("Auteurinformatie succesvol opgeslagen.",
                    "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        //controleren welke rol de gebruiker heeft
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //als er geen gebruiker is, de knoppen niet tonen
            if (App.User == null || App.User == MangaUser.Dummy)
            {
                btnSave.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                return;
            }

            var serviceProvider = App.ServiceProvider;
            if (serviceProvider == null)
            {
                btnSave.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                return;
            }

            //als de gebruiker een admin is de knoppen tonen
            var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
            bool isAdmin = await userManager.IsInRoleAsync(App.User, "Admin");

            btnSave.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            btnEdit.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

      
    }
}
