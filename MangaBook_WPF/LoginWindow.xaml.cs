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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            tbError.Text = "";

            var username = tbUsername.Text.Trim();
            var password = tbPassword.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                tbError.Text = "Vul gebruikersnaam en wachtwoord in.";
                return;
            }

            var serviceProvider = App.ServiceProvider;
            if (serviceProvider == null)
            {
                tbError.Text = "ServiceProvider niet beschikbaar.";
                return;
            }

            var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
            var user = await userManager.FindByNameAsync(username);

            if (user == null)
            {
                tbError.Text = "Gebruiker niet gevonden.";
                return;
            }

            var result = await userManager.CheckPasswordAsync(user, password);
            if (!result)
            {
                tbError.Text = "Wachtwoord is onjuist.";
                return;
            }

            //MessageBox tonen als de gebruiker geblokkeerd is
            if (user.LockoutEnd != null && user.LockoutEnd > DateTimeOffset.Now)
            {
                MessageBox.Show("Je account is geblokkeerd.", "Geblokkeerde gebruiker", MessageBoxButton.OK, MessageBoxImage.Stop);

                return;
            }


            //MainWindow openen en LoginWindow sluiten
            App.User = user;
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        //RegistratieWindow openen bij klikken van account aanmaken knop
        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            var context = App.ServiceProvider.GetRequiredService<MangaDbContext>();
            var userManager = App.ServiceProvider.GetRequiredService<UserManager<MangaUser>>();

            var registerWindow = new RegistratieWindow(context, userManager);
            registerWindow.ShowDialog();
        }
    }
}
