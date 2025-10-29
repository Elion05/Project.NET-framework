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
    /// Interaction logic for AddAuthorWindow.xaml
    /// </summary>
    public partial class AddAuthorWindow : Window
    {
        private readonly MangaDbContext _context;
        public AddAuthorWindow()
        {
            _context = new MangaDbContext();
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAddAuthor_Click(object sender, RoutedEventArgs e)
        {
            string Auteurnaam = tbAuteurNaam.Text.Trim();

            if(string.IsNullOrWhiteSpace(Auteurnaam))
            {
                MessageBox.Show("Voer een geldige auteur naam in .");
                return;
            }

            if(_context.Authors.Any(a => a.Name.ToLower() == Auteurnaam.ToLower()))
            {
                MessageBox.Show("Deze auteur bestaat al.");
            }

            var newAuthor = new Author { Name = Auteurnaam };
            _context.Authors.Add(newAuthor);
            _context.SaveChanges();

            MessageBox.Show($"Auteur '{newAuthor.Name}' is succesvol toegevoegd.");
            this.Close();

        }
    }
}
