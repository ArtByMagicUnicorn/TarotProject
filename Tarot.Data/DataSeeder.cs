using System; // <-- Ny!
using System.Collections.Generic;
using System.IO; // <-- Ny!
using System.Linq;
using System.Text.Json; // <-- Ny!

namespace Tarot.Data
{
    public static class DataSeeder
    {
        public static void Seed(TarotDbContext context)
        {
            // Steg 1 & 2: Seeda kategorier och kort
            // (Denna kod är densamma som förut)
            if (!context.MeaningCategories.Any())
            {
                var categories = new List<MeaningCategory>
                {
                    new() { CategoryName = "Upprätt" },
                    new() { CategoryName = "Omvänd" },
                    new() { CategoryName = "Dåtid" },
                    new() { CategoryName = "Nutid" },
                    new() { CategoryName = "Framtid" }
                };
                context.MeaningCategories.AddRange(categories);
                context.SaveChanges();
            }

            if (!context.TarotCards.Any())
            {
                var fullDeck = TarotDeckFactory.CreateFullDeck();
                context.TarotCards.AddRange(fullDeck);
                context.SaveChanges();
            }

            // --- HÄR BÖRJAR DEN NYA JSON-LOGIKEN ---

            // Steg 3: Seeda tolkningar (bara om det behövs)
            if (context.CardMeanings.Any())
            {
                return; // Databasen är redan fylld, gör ingenting!
            }

            try
            {
                // 1. Hitta sökvägen till vår JSON-fil
                // (Denna finns nu i mappen där programmet körs)
                var jsonFilePath = Path.Combine(AppContext.BaseDirectory, "tarot_meanings.json");

                // 2. Läs all text från filen
                var jsonText = File.ReadAllText(jsonFilePath);

                // 3. "Deserialisera" (omvandla) JSON-texten till en lista av vår C#-mall
                var meaningsToSeed = JsonSerializer.Deserialize<List<MeaningSeedDto>>(jsonText);

                // 4. Hämta alla kort och kategorier EN GÅNG (snabbt!)
                var allCards = context.TarotCards.ToDictionary(c => c.Name);
                var categories = context.MeaningCategories.ToDictionary(c => c.CategoryName);

                var allNewMeanings = new List<CardMeaning>();

                // 5. Loopa igenom datan från JSON (inte C#)
                foreach (var dto in meaningsToSeed)
                {
                    // Hitta rätt kort i vår "uppslagsbok"
                    if (allCards.TryGetValue(dto.CardName, out var card))
                    {
                        // Skapa de nya betydelse-objekten
                        allNewMeanings.Add(new CardMeaning
                        {
                            TarotCard = card,
                            MeaningCategory = categories["Upprätt"],
                            Text = dto.Upright
                        });

                        allNewMeanings.Add(new CardMeaning
                        {
                            TarotCard = card,
                            MeaningCategory = categories["Omvänd"],
                            Text = dto.Reversed
                        });
                    }
                }

                // 6. Spara ALLT till databasen i en enda, snabb operation
                context.CardMeanings.AddRange(allNewMeanings);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Om något går fel (filen hittas inte, JSON är felstavad)
                // kan vi se felet i konsolen.
                Console.WriteLine($"Kunde inte seeda tolkningar: {ex.Message}");
            }
        }
    }
}
