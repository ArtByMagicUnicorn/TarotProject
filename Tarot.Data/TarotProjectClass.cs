using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Tarot.Data
{
    public class CardMeaning
    {
        public int Id { get; set; }
        //public string Position { get; set; }
        public string Text { get; set; }
        public int CardMeaningId { get; set; }  // 👈 Främmande nyckel

        public int TarotCardId { get; set; }      // 👈 Främmande nyckel
        public TarotCard TarotCard { get; set; }  // 👈 Navigeringsegenskap
        public MeaningCategory MeaningCategory { get; set; } // 👈 Navigeringsegenskap




        public CardMeaning()
        {
        }

        public CardMeaning(string text)
        {
            Text = text;
        }
    }

    public class MeaningCategory
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


    }

    public partial class TarotCard(string name, string suit, int number)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public string Suit { get; set; } = suit;
        public int Number { get; set; } = number;
        public virtual List<CardMeaning> Meanings { get; set; } = [];
    }

    public class TarotDbContext(DbContextOptions<TarotDbContext> options) : DbContext(options)
    {
        public DbSet<TarotCard> TarotCards { get; set; }
        public DbSet<CardMeaning> CardMeanings { get; set; }
        public DbSet<MeaningCategory> MeaningCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CardMeaning>().HasKey(meaning => meaning.Id);
        }
    }

    public class TarotDeck(List<TarotCard> cards)
    {
        public List<TarotCard> TarotCards { get; set; } = cards;
    }

    public class TarotDeckFactory(TarotDbContext context)
    {
        // 1. Privat variabel för databasen
        private readonly TarotDbContext _context = context;

        // 3. En ny metod som skapar en kortlek FRÅN DATABASEN
        //    (Den gamla "CreateFullDeck" med hårdkodade kort
        //     används bara för att fylla databasen första gången, 
        //     den ska inte vara här).
        public TarotDeck CreateDeckFromDb()
        {
            // Hämta alla kort från databasen
            var cardsFromDb = _context.TarotCards.OrderBy(c => c.Id).ToList();

            // Skapa ett nytt TarotDeck-objekt med den hämtade listan
            return new TarotDeck(cardsFromDb);
        }

        public static List<TarotCard> CreateFullDeck()
        {
            List<TarotCard> deck = [];

            TarotCard fool = new("The Fool", "Major Arcana", 0);
            deck.Add(fool);
            TarotCard magician = new("The Magician", "Major Arcana", 1);
            deck.Add(magician);
            TarotCard highPriestess = new("The High Priestess", "Major Arcana", 2);
            deck.Add(highPriestess);
            TarotCard empress = new("The Empress", "Major Arcana", 3);
            deck.Add(empress);
            TarotCard emperor = new("The Emperor", "Major Arcana", 4);
            deck.Add(emperor);
            TarotCard hierophant = new("The Hierophant", "Major Arcana", 5);
            deck.Add(hierophant);
            TarotCard lovers = new("The Lovers", "Major Arcana", 6);
            deck.Add(lovers);
            TarotCard chariot = new("The Chariot", "Major Arcana", 7);
            deck.Add(chariot);
            TarotCard strength = new("Strength", "Major Arcana", 8);
            deck.Add(strength);
            TarotCard hermit = new("The Hermit", "Major Arcana", 9);
            deck.Add(hermit);
            TarotCard wheelOfFortune = new("Wheel of Fortune", "Major Arcana", 10);
            deck.Add(wheelOfFortune);
            TarotCard justice = new("Justice", "Major Arcana", 11);
            deck.Add(justice);
            TarotCard hangedMan = new("The Hanged Man", "Major Arcana", 12);
            deck.Add(hangedMan);
            TarotCard death = new("Death", "Major Arcana", 13);
            deck.Add(death);
            TarotCard temperance = new("Temperance", "Major Arcana", 14);
            deck.Add(temperance);
            TarotCard devil = new("The Devil", "Major Arcana", 15);
            deck.Add(devil);
            TarotCard tower = new("The Tower", "Major Arcana", 16);
            deck.Add(tower);
            TarotCard star = new("The Star", "Major Arcana", 17);
            deck.Add(star);
            TarotCard moon = new("The Moon", "Major Arcana", 18);
            deck.Add(moon);
            TarotCard sun = new("The Sun", "Major Arcana", 19);
            deck.Add(sun);
            TarotCard judgement = new("Judgement", "Major Arcana", 20);
            deck.Add(judgement);
            TarotCard world = new("The World", "Major Arcana", 21);
            deck.Add(world);
            string[] suits = ["Wands", "Cups", "Swords", "Pentacles"];
            foreach (string suit in suits)
            {
                for (int i = 1; i <= 14; i++)
                {
                    string name = i switch
                    {
                        1 => "Ace of " + suit,
                        11 => "Page of " + suit,
                        12 => "Knight of " + suit,
                        13 => "Queen of " + suit,
                        14 => "King of " + suit,
                        _ => i.ToString() + " of " + suit,
                    };
                    TarotCard card = new(name, suit, i);
                    deck.Add(card);
                }
            }
            return deck;
        }
    }
}
