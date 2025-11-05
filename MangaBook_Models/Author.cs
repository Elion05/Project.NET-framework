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

        //favoriete eten 

        [Display(Name = "Favoriet eten")]
        public string? favoriteFood { get; set; }

        [Display(Name = "Nationaliteit")]
        public string? Nationaliteit { get; set; }

        [Display(Name ="Favoriete Sport")]
        public string? FavorieteSport { get; set; }

        //dummy data
        public static List<Author> SeedingData()
        {
            var list = new List<Author>
            {
                new Author { Name = "Masashi Kishimoto",geboorteDatum = "8 November 1974", description = "Masashi Kishimoto is een Japanse mangaka. Zijn bekendste reeks, Naruto, verscheen van 1999 tot 2014 in het weekblad Shonen Weekly Jump en boekte wereldwijd succes. Van Naruto zijn er wereldwijd meer dan 220 miljoen exemplaren verkocht.", Nationaliteit = "Japans", favoriteFood = "Ramen", FavorieteSport = "Basketbal" },
                new Author { Name = "Eiichiro Oda", geboorteDatum = "1 Januari 1975", description = "Eiichiro Oda is een Japanse mangaka, vooral bekend als de maker van de manga One Piece. Hij werd geboren op 1 januari 1975 in Kumamoto, Japan. Oda begon al op jonge leeftijd met tekenen en publiceerde zijn eerste werk op 17-jarige leeftijd.", Nationaliteit = "Japans", favoriteFood = "Vlees", FavorieteSport = "Voetbal" },
                new Author { Name = "Hajime Isayama", geboorteDatum = "29 Augustus 1986", description = "Hajime Isayama is een Japanse mangaka, vooral bekend als de maker van de populaire manga- en anime-serie Attack on Titan (Shingeki no Kyojin). Hij werd geboren op 29 augustus 1986 in Ōyama, een stad in de prefectuur Oita, Japan.", Nationaliteit = "Japans", favoriteFood = "Gebakken kip", FavorieteSport = "Gemengde vechtsporten" },
                new Author { Name = "Koyoharu Gotouge", geboorteDatum = "5 Mei 1989", description = "Koyoharu Gotouge is een Japanse mangaka, vooral bekend als de maker van Demon Slayer: Kimetsu no Yaiba. Haar werk werd zeer populair vanwege de boeiende verhaalstructuur en de prachtige tekenstijl.", Nationaliteit = "Japans", favoriteFood = "Udon", FavorieteSport = "Badminton" },
                new Author { Name = "Kohei Horikoshi", geboorteDatum = "20 November 1986", description = "Kohei Horikoshi is een Japanse mangaka, bekend als de maker van My Hero Academia (Boku no Hero Academia). Hij werd geboren in Aichi, Japan, en zijn werk is populair vanwege de heldenconcepten en dynamische actie.", Nationaliteit = "Japans", favoriteFood = "Tonkatsu", FavorieteSport = "Honkbal" },
                new Author { Name = "Sui Ishida", geboorteDatum = "28 Januari 1986", description = "Sui Ishida is een Japanse mangaka, vooral bekend als de maker van Tokyo Ghoul. Zijn werk wordt geroemd om de donkere thema's en psychologische diepgang.", Nationaliteit = "Japans", favoriteFood = "Koffie", FavorieteSport = "Tafeltennis" },
                new Author { Name = "Tite Kubo", geboorteDatum = "26 Juni 1977", description = "Tite Kubo is een Japanse mangaka, bekend van de manga Bleach. Zijn stijl kenmerkt zich door stijlvolle gevechten en een uitgebreide wereldopbouw.", Nationaliteit = "Japans", favoriteFood = "Gegrilde vis", FavorieteSport = "Kendo" },
                new Author { Name = "Hiro Mashima", geboorteDatum = "3 Mei 1977", description = "Hiro Mashima is een Japanse mangaka, bekend als de maker van Fairy Tail. Hij staat bekend om zijn avontuurlijke verhalen en kleurrijke personages.", Nationaliteit = "Japans", favoriteFood = "Pizza", FavorieteSport = "Videogames" },
                new Author { Name = "Tsugumi Ohba", geboorteDatum = "Juli 1970", description = "Tsugumi Ohba is een Japanse mangaka, bekend als de schrijver van Death Note. Samen met illustrator Takeshi Obata creëerde hij een van de meest invloedrijke manga-series.", Nationaliteit = "Japans", favoriteFood = "Appels", FavorieteSport = "Schaken" },
                new Author { Name = "Hiromu Arakawa", geboorteDatum = "8 Mei 1973", description = "Hiromu Arakawa is een Japanse mangaka, vooral bekend als de maker van Fullmetal Alchemist. Haar werk combineert actie, avontuur en filosofische thema's.", Nationaliteit = "Japans", favoriteFood = "Aardappelen", FavorieteSport = "Volleybal" },
                new Author { Name = "Kazuki Nakashima", geboorteDatum = "17 Februari 1978", description = "Kazuki Nakashima is een Japanse scenarioschrijver en mangaka, bekend voor zijn werk aan Gurren Lagann. Hij is vooral bekend om zijn energieke verhalen en heldhaftige personages.", Nationaliteit = "Japans", favoriteFood = "Curry", FavorieteSport = "Worstelen" },
                new Author { Name = "Gege Akutami", geboorteDatum = "26 Februari 1992", description = "Gege Akutami is een Japanse mangaka, bekend als de maker van Jujutsu Kaisen. Zijn werk wordt geprezen om de complexe personages en intense gevechten.", Nationaliteit = "Japans", favoriteFood = "Mochi", FavorieteSport = "Judo" },
                new Author { Name = "Yūki Tabata", geboorteDatum = "30 Juli 1984", description = "Yūki Tabata is een Japanse mangaka, vooral bekend als de maker van Black Clover. Zijn manga combineert klassieke shonen-actie met humor en doorzettingsvermogen.", Nationaliteit = "Japans", favoriteFood = "Yakiniku", FavorieteSport = "Basketbal" },
                new Author { Name = "Kaiu Shirai", geboorteDatum = "23 Oktober 1988", description = "Kaiu Shirai is een Japanse schrijver, bekend als de auteur van The Promised Neverland. Zijn werk staat bekend om spanning en psychologische diepgang.", Nationaliteit = "Japans", favoriteFood = "Chocolade", FavorieteSport = "Lezen" },
                new Author { Name = "Muneyuki Kaneshiro", geboorteDatum = "23 April 1982", description = "Muneyuki Kaneshiro is een Japanse mangaka, vooral bekend als de schrijver van Blue Lock. Hij staat bekend om zijn intens competitieve en realistische sportverhalen.", Nationaliteit = "Japans", favoriteFood = "Steak", FavorieteSport = "Voetbal" },
                new Author { Name = "Tadatoshi Fujimaki", geboorteDatum = "9 Juni 1982", description = "Tadatoshi Fujimaki is een Japanse mangaka, bekend van Kuroko no Basket. Zijn werk draait om teamgeest en spannende sportwedstrijden.", Nationaliteit = "Japans", favoriteFood = "Cheeseburger", FavorieteSport = "Basketbal" },
                new Author { Name = "Haruichi Furudate", geboorteDatum = "7 Maart 1983", description = "Haruichi Furudate is een Japanse mangaka, bekend als de maker van Haikyuu!!. Zijn werk is geliefd vanwege de motiverende sportthema’s en sterke karakterontwikkeling.", Nationaliteit = "Japans", favoriteFood = "Gekookte rijst", FavorieteSport = "Volleybal" },
                new Author { Name = "Takehiko Inoue", geboorteDatum = "12 Januari 1967", description = "Takehiko Inoue is een Japanse mangaka, vooral bekend van Slam Dunk en Vagabond. Hij wordt geprezen om zijn realistische kunst en emotionele verhalen.", Nationaliteit = "Japans", favoriteFood = "Soba", FavorieteSport = "Basketbal" },
                new Author { Name = "Yōichi Takahashi", geboorteDatum = "28 Juli 1960", description = "Yōichi Takahashi is een Japanse mangaka, vooral bekend van Captain Tsubasa. Zijn serie inspireerde generaties voetballers wereldwijd.", Nationaliteit = "Japans", favoriteFood = "Sushi", FavorieteSport = "Voetbal" },
                new Author { Name = "Ichiro Takahashi", geboorteDatum = "14 Februari 1970", description = "Ichiro Takahashi is een Japanse mangaka, bekend van Chihayafuru. Zijn werk combineert sport, cultuur en persoonlijke groei.", Nationaliteit = "Japans", favoriteFood = "Tempura", FavorieteSport = "Karuta" },
                new Author { Name = "Natsuki Takaya", geboorteDatum = "7 Juli 1973", description = "Natsuki Takaya is een Japanse mangaka, vooral bekend als de maker van Fruits Basket. Haar werk wordt geprezen om de emotionele diepgang en karakterontwikkeling.", Nationaliteit = "Japans", favoriteFood = "Onigiri", FavorieteSport = "Wandelen" },
                new Author { Name = "Yoshihiro Togashi", geboorteDatum = "27 April 1966", description = "Yoshihiro Togashi is een Japanse mangaka, bekend van Hunter x Hunter en Yu Yu Hakusho. Zijn werk staat bekend om complexe verhaallijnen en onvoorspelbare wendingen.", Nationaliteit = "Japans", favoriteFood = "Rijst", FavorieteSport = "Bowlen" },
                new Author { Name = "Makoto Yukimura", geboorteDatum = "8 Mei 1976", description = "Makoto Yukimura is een Japanse mangaka, bekend van Vinland Saga. Zijn werk wordt geprezen om de historische nauwkeurigheid en diepgaande karakterontwikkeling.", Nationaliteit = "Japans", favoriteFood = "Vis", FavorieteSport = "Wandelen" },
                new Author { Name = "Kentaro Miura", geboorteDatum = "11 Juli 1966", description = "Kentaro Miura was een Japanse mangaka, bekend als de maker van Berserk. Zijn werk wordt geroemd om de donkere fantasy-wereld en gedetailleerde tekenstijl.", Nationaliteit = "Japans", favoriteFood = "Curry", FavorieteSport = "Videogames" },
                new Author { Name = "Hirohiko Araki", geboorteDatum = "7 Juni 1960", description = "Hirohiko Araki is een Japanse mangaka, bekend van JoJo's Bizarre Adventure. Zijn werk staat bekend om de unieke stijl, creatieve gevechten en onvergetelijke personages.", Nationaliteit = "Japans", favoriteFood = "Italiaans eten", FavorieteSport = "Film kijken" },
                new Author { Name = "Akira Toriyama", geboorteDatum = "5 April 1955", description = "Akira Toriyama was een Japanse mangaka, bekend als de maker van Dragon Ball. Zijn werk heeft een enorme invloed gehad op de manga- en anime-industrie wereldwijd.", Nationaliteit = "Japans", favoriteFood = "Gebakken kip", FavorieteSport = "Modelbouw" },
                new Author { Name = "Tatsuya Endo", geboorteDatum = "23 Juli 1980", description = "Tatsuya Endo is een Japanse mangaka, bekend van Spy x Family. Zijn werk combineert actie, komedie en spionage op een unieke manier.", Nationaliteit = "Japans", favoriteFood = "Gyoza", FavorieteSport = "Slapen" },
                new Author { Name = "Tatsuki Fujimoto", geboorteDatum = "10 Oktober 1992", description = "Tatsuki Fujimoto is een Japanse mangaka, bekend van Chainsaw Man. Zijn werk staat bekend om de onvoorspelbare verhaallijnen en duistere humor.", Nationaliteit = "Japans", favoriteFood = "Fastfood", FavorieteSport = "Films kijken" },
                new Author { Name = "ONE", geboorteDatum = "29 Oktober 1986", description = "ONE is een Japanse mangaka, bekend van One-Punch Man en Mob Psycho 100. Zijn werk wordt geprezen om de unieke humor en originele concepten.", Nationaliteit = "Japans", favoriteFood = "Vlees", FavorieteSport = "Videogames" },
                new Author { Name = "Shinichirō Watanabe", geboorteDatum = "24 Mei 1965", description = "Shinichirō Watanabe is een Japanse anime-regisseur, bekend van Cowboy Bebop en Samurai Champloo. Zijn werk staat bekend om de stilistische flair en muzikale invloeden.", Nationaliteit = "Japans", favoriteFood = "Ramen", FavorieteSport = "Muziek luisteren" },
                new Author { Name = "Naoko Takeuchi", geboorteDatum = "15 Maart 1967", description = "Naoko Takeuchi is een Japanse mangaka, bekend als de maker van Sailor Moon. Haar werk heeft het magical girl-genre gedefinieerd en wereldwijd populair gemaakt.", Nationaliteit = "Japans", favoriteFood = "Perziken", FavorieteSport = "Kunstschaatsen" },
                new Author { Name = "Rumiko Takahashi", geboorteDatum = "10 Oktober 1957", description = "Rumiko Takahashi is een Japanse mangaka, bekend van Inuyasha en Ranma ½. Ze is een van de meest productieve en succesvolle mangaka aller tijden.", Nationaliteit = "Japans", favoriteFood = "Rijstcrackers", FavorieteSport = "Honkbal kijken" },
                new Author { Name = "CLAMP", geboorteDatum = "Onbekend", description = "CLAMP is een collectief van vrouwelijke Japanse mangaka, bekend van Cardcaptor Sakura en Chobits. Hun werk staat bekend om de diverse stijlen en genres.", Nationaliteit = "Japans", favoriteFood = "Snoep", FavorieteSport = "Lezen" },
                new Author { Name = "Yana Toboso", geboorteDatum = "24 Januari 1984", description = "Yana Toboso is een Japanse mangaka, bekend van Black Butler. Haar werk wordt geprezen om de donkere sfeer en gedetailleerde tekenstijl.", Nationaliteit = "Japans", favoriteFood = "Chocolade", FavorieteSport = "Tekenen" },
                new Author { Name = "Aka Akasaka", geboorteDatum = "29 Augustus 1988", description = "Aka Akasaka is een Japanse mangaka, bekend van Kaguya-sama: Love Is War. Zijn werk staat bekend om de slimme humor en romantische verwikkelingen.", Nationaliteit = "Japans", favoriteFood = "Ramen", FavorieteSport = "Videogames" },
                new Author { Name = "Negi Haruba", geboorteDatum = "27 Juli 1991", description = "Negi Haruba is een Japanse mangaka, bekend van The Quintessential Quintuplets. Zijn werk is een populaire romantische komedie met een harem-thema.", Nationaliteit = "Japans", favoriteFood = "Curry", FavorieteSport = "Basketbal" },
                new Author { Name = "Paru Itagaki", geboorteDatum = "9 September 1993", description = "Paru Itagaki is een Japanse mangaka, bekend van Beastars. Haar werk verkent complexe sociale thema's door middel van antropomorfe dieren.", Nationaliteit = "Japans", favoriteFood = "Eieren", FavorieteSport = "Dansen" },
                new Author { Name = "Ken Wakui", geboorteDatum = "20 Januari 1978", description = "Ken Wakui is een Japanse mangaka, bekend van Tokyo Revengers. Zijn werk combineert actie, drama en tijdreizen op een spannende manier.", Nationaliteit = "Japans", favoriteFood = "Ramen", FavorieteSport = "Boksen" },
                new Author { Name = "Riichiro Inagaki", geboorteDatum = "20 Juni 1976", description = "Riichiro Inagaki is een Japanse mangaka, bekend als de schrijver van Dr. Stone en Eyeshield 21. Zijn werk staat bekend om de wetenschappelijke thema's en strategische elementen.", Nationaliteit = "Japans", favoriteFood = "Vlees", FavorieteSport = "American Football" },
                new Author { Name = "Boichi", geboorteDatum = "29 Januari 1973", description = "Boichi is een Zuid-Koreaanse mangaka die in Japan werkt, bekend als de tekenaar van Dr. Stone. Zijn tekenstijl is zeer gedetailleerd en dynamisch.", Nationaliteit = "Zuid-Koreaans", favoriteFood = "Kimchi", FavorieteSport = "Gewichtheffen" },
                new Author { Name = "Yusuke Murata", geboorteDatum = "4 Juli 1978", description = "Yusuke Murata is een Japanse mangaka, bekend als de tekenaar van One-Punch Man en Eyeshield 21. Hij wordt geprezen om zijn uitzonderlijke tekenvaardigheid.", Nationaliteit = "Japans", favoriteFood = "Steak", FavorieteSport = "Videogames" }
            };
            return list;
        }
    }
}
