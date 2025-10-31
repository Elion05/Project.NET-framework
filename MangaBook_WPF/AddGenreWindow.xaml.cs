using MangaBook_Models;
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
    /// Interaction logic for AddGenreWindow.xaml
    /// </summary>
    public partial class AddGenreWindow : Window
    {
        private readonly MangaDbContext _context;
        public AddGenreWindow(MangaDbContext context)
        {
            InitializeComponent();
            _context = context;
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddGenre_Click(object sender, RoutedEventArgs e)
        {
            string naam = tbGenreName.Text.Trim();

            if (string.IsNullOrWhiteSpace(naam))
            {
                MessageBox.Show("Voer een geldige genre naam in .");
                return;
            }
            if(_context.Genres.Any(g => g.Name.ToLower() == naam.ToLower()))
            {
                MessageBox.Show("Deze genre bestaat al.");
                return;
            }

            var newGenre = new Genre { Name = naam };
            _context.Genres.Add(newGenre);
            _context.SaveChanges();

            MessageBox.Show($"Genre '{newGenre.Name}' is succesvol toegevoegd.", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            
            this.Close();

        }
    }
}
