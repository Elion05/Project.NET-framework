using System;
using MangaBook_Models;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaBook_Models
{
    public class MangaBook
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Beschrijving")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; } = string.Empty;


       public bool IsDeleted { get; set; } = false;

        [Required]
        [Display(Name = "Publicatiedatum")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; } = DateTime.Now;

        [Required]
        [Display(Name = "Auteur")]
        [ForeignKey("Author")]
        public int AuthorId { get; set; }
        public Author? Author { get; set; }


        [Required]
        [Display(Name = "Genre")]
        [ForeignKey("Genre")]
        public int GenreId { get; set; }

        public Genre? Genre { get; set; }



        //dummy data
        public static List<MangaBook> SeedingData()
        {
            var list = new List<MangaBook>();

            list.AddRange( 
            new MangaBook
            {
                Title = "Naruto",
                Description = "Naruto Uzumaki, a determined young ninja with a sealed fox spirit, trains to gain respect and become Hokage.",
                ReleaseDate = new DateTime(1999, 9, 21),
                AuthorId = 1,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "One Piece",
                Description = "Monkey D. Luffy, a rubber-bodied pirate, sails the Grand Line seeking the legendary treasure One Piece and freedom.",
                ReleaseDate = new DateTime(1997, 7, 22),
                AuthorId = 2,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Attack on Titan",
                Description = "Humanity fights gigantic Titans behind massive walls; Eren Yeager joins the military to uncover the truth and survive.",
                ReleaseDate = new DateTime(2009, 9, 9),
                AuthorId = 3,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Demon Slayer",
                Description = "Tanjiro becomes a demon slayer to avenge his family and find a cure for his sister's condition.",
                ReleaseDate = new DateTime(2016, 2, 15),
                AuthorId = 4,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "My Hero Academia",
                Description = "In a world of Quirks, powerless Izuku receives a great power and trains to become a true hero.",
                ReleaseDate = new DateTime(2014, 7, 7),
                AuthorId = 5,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Tokyo Ghoul",
                Description = "Ken Kaneki becomes a half-ghoul and struggles with identity, hunger, and violent conflicts between humans and ghouls.",
                ReleaseDate = new DateTime(2011, 9, 8),
                AuthorId = 6,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Bleach",
                Description = "Ichigo gains Soul Reaper powers, battles Hollows, and protects the living while learning about the spirit world.",
                ReleaseDate = new DateTime(2001, 8, 7),
                AuthorId = 7,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Boruto",
                Description = "Boruto Uzumaki copes with his father’s legacy while forging his own path amid modern challenges.",
                ReleaseDate = new DateTime(2016, 5, 9),
                AuthorId = 1,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Fairy Tail",
                Description = "Natsu and his guildmates undertake dangerous missions, strengthen their bonds, and fight dark forces together.",
                ReleaseDate = new DateTime(2006, 8, 2),
                AuthorId = 8,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Death Note",
                Description = "Light Yagami finds a deadly notebook and starts a tense battle of wits with a brilliant detective over justice.",
                ReleaseDate = new DateTime(2003, 12, 1),
                AuthorId = 9,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Fullmetal Alchemist",
                Description = "Brothers Edward and Alphonse use alchemy to restore their bodies, facing political intrigue and moral consequences.",
                ReleaseDate = new DateTime(2001, 7, 12),
                AuthorId = 10,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Gurren Lagann",
                Description = "Simon and Kamina discover a giant mecha, inspire humanity, and battle to reclaim the surface from oppression.",
                ReleaseDate = new DateTime(2007, 4, 1),
                AuthorId = 11,
                GenreId = 6
            },
            new MangaBook
            {
                Title = "Jujutsu Kaisen",
                Description = "Yuji Itadori swallows a cursed object, joins sorcerers, and learns about sacrifice while fighting deadly curses.",
                ReleaseDate = new DateTime(2018, 3, 5),
                AuthorId = 12,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Black Clover",
                Description = "Asta, born without magic, aims to become the Wizard King through sheer determination and hard work.",
                ReleaseDate = new DateTime(2015, 2, 16),
                AuthorId = 13,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "The Promised Neverland",
                Description = "Emma and her friends uncover dark secrets about their orphanage and plan a daring escape to freedom.",
                ReleaseDate = new DateTime(2016, 8, 1),
                AuthorId = 14,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Blue Lock",
                Description = "Japanese soccer players compete in a high-stakes program to create the ultimate striker.",
                ReleaseDate = new DateTime(2018, 8, 1),
                AuthorId = 15,
                GenreId = 8
            },
            new MangaBook
            {
                Title = "Kuroko's Basketball",
                Description = "Kuroko and his teammates strive to become Japan's top high school basketball team through teamwork and skill.",
                ReleaseDate = new DateTime(2008, 12, 8),
                AuthorId = 16,
                GenreId = 8
            },
            new MangaBook
            {
                Title = "Haikyuu!!",
                Description = "Shoyo Hinata overcomes his small stature to lead his high school volleyball team to new heights.",
                ReleaseDate = new DateTime(2012, 2, 20),
                AuthorId = 17,
                GenreId = 8
            },
            new MangaBook
            {
                Title = "Slam Dunk",
                Description = "Hanamichi Sakuragi discovers a passion for basketball, transforming from delinquent to team leader.",
                ReleaseDate = new DateTime(1990, 10, 1),
                AuthorId = 18,
                GenreId = 8
            },
            new MangaBook
            {
                Title = "Captain Tsubasa",
                Description = "Tsubasa Ozora dreams of winning the World Cup, inspiring teammates and rivals with his soccer skills.",
                ReleaseDate = new DateTime(1981, 10, 1),
                AuthorId = 19,
                GenreId = 8
            },
            new MangaBook
            {
                Title = "Code Geass",
                Description = "In an alternate future, exiled prince Lelouch gains a power to command anyone and leads a rebellion against an empire.",
                ReleaseDate = new DateTime(2006, 10, 6),
                AuthorId = 20,
                GenreId = 6
            },
            new MangaBook
            {
                Title = "Fruit Basket",
                Description = "Tohru Honda discovers the Sohma family's curse, forming deep bonds while navigating love and acceptance.",
                ReleaseDate = new DateTime(1998, 7, 18),
                AuthorId = 21,
                GenreId = 2
            },
            new MangaBook
            {
                Title = "Hunter x Hunter",
                Description = "Gon Freecss aims to become a Hunter to find his father, embarking on an adventure with friends and facing powerful foes.",
                ReleaseDate = new DateTime(1998, 3, 3),
                AuthorId = 22,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Vinland Saga",
                Description = "Thorfinn seeks revenge for his father's death, joining a band of Vikings and exploring themes of war, peace, and purpose.",
                ReleaseDate = new DateTime(2005, 4, 13),
                AuthorId = 23,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Berserk",
                Description = "Guts, a lone mercenary, battles demons and corrupt nobles in a dark fantasy world, seeking revenge and redemption.",
                ReleaseDate = new DateTime(1989, 8, 25),
                AuthorId = 24,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "JoJo's Bizarre Adventure",
                Description = "The Joestar family's lineage is marked by supernatural abilities and epic battles against evil forces through generations.",
                ReleaseDate = new DateTime(1987, 1, 1),
                AuthorId = 25,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Dragon Ball",
                Description = "Goku, a powerful Saiyan, protects Earth from powerful foes, constantly training and pushing his limits.",
                ReleaseDate = new DateTime(1984, 11, 20),
                AuthorId = 26,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Spy x Family",
                Description = "A spy, an assassin, and a telepath form a makeshift family for a mission, navigating the challenges of their double lives.",
                ReleaseDate = new DateTime(2019, 3, 25),
                AuthorId = 27,
                GenreId = 9
            },
            new MangaBook
            {
                Title = "Chainsaw Man",
                Description = "Denji, a young man merged with a devil, becomes a devil hunter to pay off his debts and live a normal life.",
                ReleaseDate = new DateTime(2018, 12, 3),
                AuthorId = 28,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "One-Punch Man",
                Description = "Saitama, a hero who can defeat any enemy with a single punch, faces the existential crisis of being too strong.",
                ReleaseDate = new DateTime(2012, 6, 14),
                AuthorId = 29,
                GenreId = 9
            },
            new MangaBook
            {
                Title = "Mob Psycho 100",
                Description = "Shigeo 'Mob' Kageyama, a powerful psychic, tries to control his emotions to prevent his powers from going berserk.",
                ReleaseDate = new DateTime(2012, 4, 18),
                AuthorId = 29,
                GenreId = 9
            },
            new MangaBook
            {
                Title = "Cowboy Bebop",
                Description = "A group of bounty hunters travels the solar system in their spaceship, the Bebop, confronting their pasts along the way.",
                ReleaseDate = new DateTime(1997, 9, 1),
                AuthorId = 30,
                GenreId = 10
            },
            new MangaBook
            {
                Title = "Samurai Champloo",
                Description = "Two rival swordsmen and a young woman search for a mysterious samurai, blending historical Japan with modern elements.",
                ReleaseDate = new DateTime(2004, 5, 20),
                AuthorId = 30,
                GenreId = 11
            },
            new MangaBook
            {
                Title = "Sailor Moon",
                Description = "Usagi Tsukino, a clumsy schoolgirl, transforms into Sailor Moon to fight evil and protect the universe with her friends.",
                ReleaseDate = new DateTime(1991, 12, 28),
                AuthorId = 31,
                GenreId = 12
            },
            new MangaBook
            {
                Title = "Inuyasha",
                Description = "Kagome Higurashi is transported to feudal Japan, where she teams up with the half-demon Inuyasha to find sacred jewel shards.",
                ReleaseDate = new DateTime(1996, 11, 13),
                AuthorId = 32,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Cardcaptor Sakura",
                Description = "Sakura Kinomoto accidentally releases magical cards and must become a Cardcaptor to retrieve them.",
                ReleaseDate = new DateTime(1996, 5, 1),
                AuthorId = 33,
                GenreId = 12
            },
            new MangaBook
            {
                Title = "Black Butler",
                Description = "A young earl makes a pact with a demon butler to avenge his parents' deaths in Victorian England.",
                ReleaseDate = new DateTime(2006, 9, 16),
                AuthorId = 34,
                GenreId = 3
            },
            new MangaBook
            {
                Title = "Kaguya-sama: Love Is War",
                Description = "Two genius high school students engage in a battle of wits to make the other confess their love first.",
                ReleaseDate = new DateTime(2015, 5, 19),
                AuthorId = 35,
                GenreId = 9
            },
            new MangaBook
            {
                Title = "The Quintessential Quintuplets",
                Description = "A high school student is hired to tutor five identical quintuplets who are on the verge of failing.",
                ReleaseDate = new DateTime(2017, 8, 9),
                AuthorId = 36,
                GenreId = 13
            },
            new MangaBook
            {
                Title = "Beastars",
                Description = "In a world of anthropomorphic animals, a gentle wolf grapples with his predatory instincts and a complex murder mystery.",
                ReleaseDate = new DateTime(2016, 9, 8),
                AuthorId = 37,
                GenreId = 14
            },
            new MangaBook
            {
                Title = "Tokyo Revengers",
                Description = "A down-on-his-luck man travels back in time to his middle school days to save his ex-girlfriend from a gang.",
                ReleaseDate = new DateTime(2017, 3, 1),
                AuthorId = 38,
                GenreId = 1
            },
            new MangaBook
            {
                Title = "Dr. Stone",
                Description = "A genius high school student awakens in a world where humanity has been turned to stone and aims to rebuild civilization with science.",
                ReleaseDate = new DateTime(2017, 3, 6),
                AuthorId = 39,
                GenreId = 10
            },
            new MangaBook
            {
                Title = "Eyeshield 21",
                Description = "A timid boy with incredible running speed is forced to join an American football team as their secret weapon.",
                ReleaseDate = new DateTime(2002, 7, 23),
                AuthorId = 39,
                GenreId = 8
            }
            );
            return list;
        }
    }
}
