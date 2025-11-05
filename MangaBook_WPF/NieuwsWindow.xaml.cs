using MangaBook_Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for NieuwsWindow.xaml
    /// </summary>
    public partial class NieuwsWindow : Window
    {
        private readonly MangaDbContext _context;
        private bool _isEditing = false;
        private Nieuws_Bericht? _geselecteerdBericht;
        public NieuwsWindow()
        {
            InitializeComponent();
            _context = new MangaDbContext();
            LoadNieuws();
        }

        private void LoadNieuws()
        {
            var nieuwsLijst = _context.Nieuws_Berichten
                .Where(n => !n.isVerwijderd)
                .Include(n => n.Gebruiker)
                .OrderByDescending(n => n.Datum)
                .ToList();
            NieuwsListBox.ItemsSource = nieuwsLijst;

            btnEdit.IsEnabled = false;
            btnDelete.IsEnabled = false;
        }
        private void NieuwsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _geselecteerdBericht = NieuwsListBox.SelectedItem as Nieuws_Bericht;

            bool heeftSelectie = _geselecteerdBericht != null;
            btnEdit.IsEnabled = NieuwsListBox.SelectedItem != null;
            btnDelete.IsEnabled = NieuwsListBox.SelectedItem != null;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            brDetails.Visibility = Visibility.Visible;
            tbTitle.Text = string.Empty;
            tbInhoud.Text = string.Empty;
            _isEditing = false;
            _geselecteerdBericht = null;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (_geselecteerdBericht == null)
                return;

            brDetails.Visibility = Visibility.Visible;
            tbTitle.Text = _geselecteerdBericht.Titel;
            tbInhoud.Text = _geselecteerdBericht.Inhoud;

            _isEditing = true;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_geselecteerdBericht == null)
                return;

            var bevestiging = MessageBox.Show($"Weet je zeker dat je '{_geselecteerdBericht.Titel}' wilt verwijderen?",
                "Bevestigen", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if(bevestiging == MessageBoxResult.Yes)
            {
                _geselecteerdBericht.isVerwijderd = true;
                _context.SaveChanges();
                LoadNieuws();
                MessageBox.Show("Bericht succesvol verwijderd.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            string titel = tbTitle.Text.Trim();
            string inhoud = tbInhoud.Text.Trim();

            if (string.IsNullOrWhiteSpace(titel) || string.IsNullOrWhiteSpace(inhoud))
            {
                MessageBox.Show("Titel en inhoud zijn verplicht.", "Fout", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_isEditing && _geselecteerdBericht != null)
            {
                // Bewerken
                _geselecteerdBericht.Titel = titel;
                _geselecteerdBericht.Inhoud = inhoud;
                _context.SaveChanges();
                MessageBox.Show("Bericht bijgewerkt.", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                //bericht aamnaken
                var nieuw = new Nieuws_Bericht
                {
                    Titel = titel,
                    Inhoud = inhoud,
                    Datum = DateTime.Now,
                    GebruikerId = App.User?.Id ?? "", //ingelogde gebruiker
                    isVerwijderd = false
                };

                _context.Nieuws_Berichten.Add(nieuw);
                _context.SaveChanges();
                MessageBox.Show("Nieuw bericht toegevoegd!", "Succes", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            brDetails.Visibility = Visibility.Collapsed;
            LoadNieuws();
        }


        //zelfde window_loaded zoals bij all de ander windows waar admin rechten nodig zijn
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (App.User != null && App.User != MangaUser.Dummy)
            {
                var serviceProvider = App.ServiceProvider;
                if (serviceProvider == null)
                {
                    btnAdd.Visibility = Visibility.Collapsed;
                    btnEdit.Visibility = Visibility.Collapsed;
                    btnDelete.Visibility = Visibility.Collapsed;
                    return;
                }

                var userManager = serviceProvider.GetRequiredService<UserManager<MangaUser>>();
                bool isAdmin = await userManager.IsInRoleAsync(App.User, "Admin");

                btnAdd.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnEdit.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
                btnDelete.Visibility = isAdmin ? Visibility.Visible : Visibility.Collapsed;
            }
            else
            {
                //als niemand is ingelogd,niet tonen
                btnAdd.Visibility = Visibility.Collapsed;
                btnEdit.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
            }
        }
    }
}

