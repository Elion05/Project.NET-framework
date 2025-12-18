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
using MangaBook_Models;
using MangaBook_Models.NewFolder;
using Microsoft.AspNetCore.Identity;

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for GebruikersProfiel.xaml
    /// </summary>
    public partial class GebruikersProfiel : Window
    {
        private MangaDbContext _context;
        private UserManager<MangaUser> _userManager;
        private MangaUser? _currentUser;

        public GebruikersProfiel(MangaDbContext context, UserManager<MangaUser> userManager)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;
        }


        //opslaan profiel
        private async void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {

            if(_currentUser == null)
            {
                MessageBox.Show("Geen gebruiker geladen.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //gebruikersgegevens bijwerken, de informatie staat al direct in de textboxes
            _currentUser.UserName = tbGebruikersnaam.Text;
            _currentUser.FirstName = tbVoornaam.Text;
            _currentUser.LastName = tbAchternaam.Text;
            _currentUser.Email = tbEmail.Text;

            //Alleen wachtwoord bijwerken als er iets anders is ingevuld dan "********"
            if (!string.IsNullOrWhiteSpace(pbWachtwoord.Password) && pbWachtwoord.Password != "********")
            {
                var newPasswordHash = _userManager.PasswordHasher.HashPassword(_currentUser, pbWachtwoord.Password);
                _currentUser.PasswordHash = newPasswordHash;
            }

            var result = await _userManager.UpdateAsync(_currentUser);


            if (result.Succeeded)
            {
                MessageBox.Show("Gebruikersprofiel bijgewerkt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                string fout = string.Join("\n", result.Errors.Select(err => err.Description));
                MessageBox.Show($"Fout bij bijwerken gebruikersprofiel:\n{fout}", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private  async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if(App.User == null || App.User == MangaUser.Dummy)
            {
                MessageBox.Show("Er is geen gebruiker ingelogd.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
                return;
            }
            _currentUser = await _userManager.FindByIdAsync(App.User.Id);

            if(_currentUser != null)
            {
                tbGebruikersnaam.Text = _currentUser.UserName;
                tbEmail.Text = _currentUser.Email;
                pbWachtwoord.Password = "********";
                tbVoornaam.Text = _currentUser.FirstName;
                tbAchternaam.Text = _currentUser.LastName;
            }
            else
            {
                MessageBox.Show("De ingelogde gebruiker kon niet gevonden worden.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }
    }
}
