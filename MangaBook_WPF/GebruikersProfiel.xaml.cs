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

namespace MangaBook_WPF
{
    /// <summary>
    /// Interaction logic for GebruikersProfiel.xaml
    /// </summary>
    public partial class GebruikersProfiel : Window
    {
        private MangaDbContext _context;
        private UserManager<MangaUser> _userManager;

        public GebruikersProfiel(MangaDbContext context, UserManager<MangaUser> userManager)
        {
            InitializeComponent();
            _context = context;
            _userManager = userManager;
        }

        private void btnSaveProfile_Click(object sender, RoutedEventArgs e)
        {
            //testen of de knop werkt
            MessageBox.Show("Profiel succesvol opgeslagen!", "Informatie", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();

        }
    }
}
