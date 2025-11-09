using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using System.Windows;


namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for RegistratieWindow.xaml
    ////6) Registratie
    ///</summary>
    public partial class RegistratieWindow : Window
    {
        private readonly MangaDbContext _context = new MangaDbContext();
        private readonly UserManager<MangaUser> _userManager;
        public RegistratieWindow(MangaDbContext context, UserManager<MangaUser> userManager)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;
            tbUsername.Focus();
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            tbError.Text = ""; //foutmelding leegmaken

            if (string.IsNullOrWhiteSpace(tbUsername.Text))
            {
                tbError.Text = "Vul jouw gebruikersnaam in.";
                tbUsername.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbFirstName.Text))
            {
                tbError.Text = "Vul jouw voornaam in.";
                tbFirstName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbLastName.Text))
            {
                tbError.Text = "Vul jouw achternaam in.";
                tbLastName.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(tbEmail.Text))
            {
                tbError.Text = "Vul jouw e-mailadres in.";
                tbEmail.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(pbPassword.Password))
            {
                tbError.Text = "Vul jouw wachtwoord in.";
                pbPassword.Focus();
                return;
            }

            if (pbPassword.Password != pbConfirmPassword.Password)
            {
                tbError.Text = "Wachtwoorden komen niet overeen.";
                pbConfirmPassword.Clear();
                pbPassword.Focus();
                return;
            }

            //controlleer of gebruikersnaam al genomen is
            var bestaandeUserName = await _userManager.FindByNameAsync(tbUsername.Text);
            if (bestaandeUserName != null)
            {
                tbError.Text = "Deze gebruikersnaam is al gebruikt.";
                tbUsername.Focus();
                return ;
            }

            //hetzelfde als bij de gebruikersnaam maar dan met email
            var gebruikteEmail = await _userManager.FindByEmailAsync(tbEmail.Text);
            if (gebruikteEmail != null)
            {
                tbError.Text = "Deze email is al in gebruik, geef een andere email";
                tbEmail.Focus();
                return ;
            }


            //maak nieuwe gebruiker aan de hand van de ingevoerde gegevens
            var newGebruiker = new MangaUser
            {
                UserName = tbUsername.Text,
                FirstName = tbFirstName.Text,
                LastName = tbLastName.Text,
                Email = tbEmail.Text,
                EmailConfirmed = true,
                LockoutEnabled = false,
                TwoFactorEnabled = false
            };


            //probeer gebruiker aan te maken met wachtwoord
            var resultaat = await _userManager.CreateAsync(newGebruiker, pbPassword.Password);

            if (resultaat.Succeeded)
            {
                //default role is User
                await _userManager.AddToRoleAsync(newGebruiker, "User");

                MessageBox.Show("Je account is succesvol aangemaakt!", "Registratie voltooid", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                //foutmelding tonen als registratie mislukt
                tbError.Text = string.Join("\n", resultaat.Errors.Select(e => e.Description));

            }
        }
    }
}
