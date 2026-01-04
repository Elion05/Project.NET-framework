using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MangaBook_Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;


namespace Manga_App.ViewModels
{
    public partial class Nieuws_BerichtViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Nieuws_Bericht> nieuwsBerichten = new();
        Nieuws_Bericht nieuwsBericht;

        readonly LocalDbContext _context;
        readonly Synchronizer _synchronizer;

        //voor de knop om te sorteren
        private bool sortDescending = true;

        //Constructor om parameters te accepteren
        public Nieuws_BerichtViewModel(Nieuws_Bericht bericht, LocalDbContext context)
        {
            nieuwsBericht = bericht;
            _context = context;
            _synchronizer = new Synchronizer(context);
        }

        [RelayCommand]
        public async Task LoadNieuwsBerichten()
        {
            var berichten = await _synchronizer.GetNieuwsBerichtenFromApiAsync();

            if (berichten.Any())
            {
                NieuwsBerichten.Clear();
                foreach (var bericht in berichten)
                {
                    NieuwsBerichten.Add(bericht);
                }
            }
        }

        //sorteer functie
        [RelayCommand]
        public void Sorteren()
        {
            if (NieuwsBerichten.Count == 0)
                return;

            if (sortDescending)
            {
                NieuwsBerichten = new ObservableCollection<Nieuws_Bericht>(
                    NieuwsBerichten.OrderByDescending(n => n.Datum));
            }
            else
            {
                NieuwsBerichten = new ObservableCollection<Nieuws_Bericht>(
                    NieuwsBerichten.OrderBy(n => n.Datum));
            }
            sortDescending = !sortDescending;
        }
    }
}
