using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Design;

namespace MangaBook_Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Auteur naam")]
        public string Name { get; set; } = string.Empty;


        [Required]
        [Display(Name = "Geboorte datum")]
        [DataType(DataType.Date)]
        public string geboorteDatum { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Beschrijving")]
        [DataType(DataType.MultilineText)]
        public string description { get; set; } = string.Empty;



        //dummy data
        public static List<Author> SeedingData()
        {
            var list = new List<Author>
            {
                new Author { Name = "Masashi Kishimoto", geboorteDatum = "8 November 1974", description = "Masashi Kishimoto is een Japanse mangaka. Zijn bekendste reeks, Naruto, verscheen van 1999 tot 2014 in het weekblad Shonen Weekly Jump en boekte wereldwijd succes. Van Naruto zijn er wereldwijd meer dan 220 miljoen exemplaren verkocht." },
                new Author { Name = "Eiichiro Oda", geboorteDatum = "1 Januari 1975", description = "Eiichiro Oda is een Japanse mangaka, vooral bekend als de maker van de manga One Piece. Hij werd geboren op 1 januari 1975 in Kumamoto, Japan. Oda begon al op jonge leeftijd met tekenen en publiceerde zijn eerste werk op 17-jarige leeftijd." },
                new Author { Name = "Hajime Isayama", geboorteDatum = "29 Augustus 1986", description = "Hajime Isayama is een Japanse mangaka, vooral bekend als de maker van de populaire manga- en anime-serie Attack on Titan (Shingeki no Kyojin). Hij werd geboren op 29 augustus 1986 in Ōyama, een stad in de prefectuur Oita, Japan." },
                new Author { Name = "Koyoharu Gotouge", geboorteDatum = "5 Mei 1989", description = "Koyoharu Gotouge is een Japanse mangaka, vooral bekend als de maker van Demon Slayer: Kimetsu no Yaiba. Haar werk werd zeer populair vanwege de boeiende verhaalstructuur en de prachtige tekenstijl." },
                new Author { Name = "Kohei Horikoshi", geboorteDatum = "20 November 1986", description = "Kohei Horikoshi is een Japanse mangaka, bekend als de maker van My Hero Academia (Boku no Hero Academia). Hij werd geboren in Aichi, Japan, en zijn werk is populair vanwege de heldenconcepten en dynamische actie." },
                new Author { Name = "Sui Ishida", geboorteDatum = "28 Januari 1986", description = "Sui Ishida is een Japanse mangaka, vooral bekend als de maker van Tokyo Ghoul. Zijn werk wordt geroemd om de donkere thema's en psychologische diepgang." },
                new Author { Name = "Tite Kubo", geboorteDatum = "26 Juni 1977", description = "Tite Kubo is een Japanse mangaka, bekend van de manga Bleach. Zijn stijl kenmerkt zich door stijlvolle gevechten en een uitgebreide wereldopbouw." },
                new Author { Name = "Hiro Mashima", geboorteDatum = "3 Mei 1977", description = "Hiro Mashima is een Japanse mangaka, bekend als de maker van Fairy Tail. Hij staat bekend om zijn avontuurlijke verhalen en kleurrijke personages." },
                new Author { Name = "Tsugumi Ohba", geboorteDatum = "Juli 1970", description = "Tsugumi Ohba is een Japanse mangaka, bekend als de schrijver van Death Note. Samen met illustrator Takeshi Obata creëerde hij een van de meest invloedrijke manga-series." },
                new Author { Name = "Hiromu Arakawa", geboorteDatum = "8 Mei 1973", description = "Hiromu Arakawa is een Japanse mangaka, vooral bekend als de maker van Fullmetal Alchemist. Haar werk combineert actie, avontuur en filosofische thema's." },
                new Author { Name = "Kazuki Nakashima", geboorteDatum = "17 Februari 1978", description = "Kazuki Nakashima is een Japanse scenarioschrijver en mangaka, bekend voor zijn werk aan Gurren Lagann. Hij is vooral bekend om zijn energieke verhalen en heldhaftige personages." },
                new Author { Name = "Gege Akutami", geboorteDatum = "26 Februari 1992", description = "Gege Akutami is een Japanse mangaka, bekend als de maker van Jujutsu Kaisen. Zijn werk wordt geprezen om de complexe personages en intense gevechten." },
                new Author { Name = "Yūki Tabata", geboorteDatum = "30 Juli 1984", description = "Yūki Tabata is een Japanse mangaka, vooral bekend als de maker van Black Clover. Zijn manga combineert klassieke shonen-actie met humor en doorzettingsvermogen." },
                new Author { Name = "Kaiu Shirai", geboorteDatum = "23 Oktober 1988", description = "Kaiu Shirai is een Japanse schrijver, bekend als de auteur van The Promised Neverland. Zijn werk staat bekend om spanning en psychologische diepgang." },
                new Author { Name = "Muneyuki Kaneshiro", geboorteDatum = "23 April 1982", description = "Muneyuki Kaneshiro is een Japanse mangaka, vooral bekend als de schrijver van Blue Lock. Hij staat bekend om zijn intens competitieve en realistische sportverhalen." },
                new Author { Name = "Tadatoshi Fujimaki", geboorteDatum = "9 Juni 1982", description = "Tadatoshi Fujimaki is een Japanse mangaka, bekend van Kuroko no Basket. Zijn werk draait om teamgeest en spannende sportwedstrijden." },
                new Author { Name = "Haruichi Furudate", geboorteDatum = "7 Maart 1983", description = "Haruichi Furudate is een Japanse mangaka, bekend als de maker van Haikyuu!!. Zijn werk is geliefd vanwege de motiverende sportthema’s en sterke karakterontwikkeling." },
                new Author { Name = "Takehiko Inoue", geboorteDatum = "12 Januari 1967", description = "Takehiko Inoue is een Japanse mangaka, vooral bekend van Slam Dunk en Vagabond. Hij wordt geprezen om zijn realistische kunst en emotionele verhalen." },
                new Author { Name = "Yōichi Takahashi", geboorteDatum = "28 Juli 1960", description = "Yōichi Takahashi is een Japanse mangaka, vooral bekend van Captain Tsubasa. Zijn serie inspireerde generaties voetballers wereldwijd." },
                new Author {Name = "Ichiro Takahashi", geboorteDatum = "14 Februari 1970", description = "Ichiro Takahashi is een Japanse mangaka, bekend van Chihayafuru. Zijn werk combineert sport, cultuur en persoonlijke groei." },
                new Author { Name = "Natsuki Takaya", geboorteDatum = "7 Juli 1973", description = "Natsuki Takaya is een Japanse mangaka, vooral bekend als de maker van Fruits Basket. Haar werk wordt geprezen om de emotionele diepgang en karakterontwikkeling." }
            };
            return list;
        }
        public override string ToString()
        {
            return Name;
        }

    }
}
