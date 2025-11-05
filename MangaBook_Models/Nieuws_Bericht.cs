
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaBook_Models
{
    public class Nieuws_Bericht
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(40)]
        public string Titel { get; set; } = string.Empty;

        [Required, MaxLength(300)]
        public string Inhoud { get; set; } = string.Empty;

        [Required]
        public DateTime Datum { get; set; } = DateTime.Now;

        public string GebruikerId { get; set; } = string.Empty;

        [ForeignKey(nameof(GebruikerId))]
        public MangaUser? Gebruiker { get; set; }

        public bool isVerwijderd { get; set; } = false;
    }
}
