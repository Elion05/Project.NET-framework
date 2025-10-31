using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for DetailsGenreWindow.xaml
    /// </summary>
    public partial class DetailsGenreWindow : Window
    {
        private readonly Genre _genre;
        private readonly MangaDbContext _context;
        public DetailsGenreWindow(Genre genre, MangaDbContext context)
        {
            InitializeComponent();
            _genre = genre;
            _context = context;
            DataContext = _genre;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (_genre != null)
            {
                // Nieuwe waarden opslaan
                _genre.Name = tbGenreNaam.Text;
                _genre.genreBeschrijving = tbGenreBeschrijving.Text;

                // Opslaan in database
                _context.Genres.Update(_genre);
                _context.SaveChanges();

                // UI verversen
                brEditGenreDetails.Visibility = Visibility.Collapsed;
                DataContext = null;
                DataContext = _genre;

                MessageBox.Show("Genre-informatie succesvol opgeslagen.",
                    "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            brEditGenreDetails.Visibility = Visibility.Visible;

            // Velden invullen met huidige gegevens
            tbGenreNaam.Text = _genre.Name;
            tbGenreBeschrijving.Text = _genre.genreBeschrijving;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //als er geen gebruiker is, de knoppen niet tonen
            if (App.User == null || App.User == MangaUser.Dummy)
            {
                btnEditGenre.Visibility = Visibility.Collapsed;
                btnSave.Visibility = Visibility.Collapsed;
                return;
            }

            var serviceProvider = App.ServiceProvider;
            if (serviceProvider == null)
            {
                btnSave.Visibility = Visibility.Collapsed;
                btnEditGenre.Visibility = Visibility.Collapsed;
                return;
            }

            //als de gebruiker een admin is de knoppen tonen
            var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
            bool isAdmin = await userManager.IsInRoleAsync(App.User, "Admin");

            btnEditGenre.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            btnSave.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        private void OpslaanKnop_Click(object sender, RoutedEventArgs e)
        {
            if(_genre != null)
            {
                _genre.Name = tbGenreNaam.Text;
                _genre.genreBeschrijving = tbGenreBeschrijving.Text;

                _context.Genres.Update(_genre);
                _context.SaveChanges();

                brEditGenreDetails.Visibility = Visibility.Collapsed;

                MessageBox.Show("Genre-informatie succesvol opgeslagen.",
                    "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
        }

        private void btnEditGenre_Click(object sender, RoutedEventArgs e)
        {
            brEditGenreDetails.Visibility = Visibility.Visible;
        }
    }
}
