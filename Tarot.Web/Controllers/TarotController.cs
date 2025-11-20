using Microsoft.AspNetCore.Mvc;
using Tarot.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Tarot.Web.Controllers
{
    public class TarotController(TarotDbContext context) : Controller
    {
        private readonly TarotDbContext _context = context;

        // 5. Ändra din Index-metod att hämta data
        public async Task<IActionResult> Index()
        {
            // Hämta alla kort från databasen, sorterade efter deras ID
            var allCards = await _context.TarotCards
                                         .OrderBy(card => card.Id)
                                         .ToListAsync();

            return View(allCards);
        }
        // Lägg till denna nya metod i TarotController.cs

        public async Task<IActionResult> DrawToday()
        {
            var randomCard = await _context.TarotCards


                .Include(c => c.Meanings)       // <-- 1. Hämta alla betydelser för kortet
                    .ThenInclude(m => m.MeaningCategory) // <-- 2. Hämta kategorin FÖR VARJE betydelse

                .OrderBy(c => EF.Functions.Random())
                .FirstOrDefaultAsync();

            if (randomCard == null)
            {
                return NotFound("Kunde inte hitta några kort i leken!");
            }

            return View(randomCard);
        }

        // Lägg till denna nya metod i TarotController.cs

        public async Task<IActionResult> ThreeCardSpread()
        {
            // Precis som förut, men vi använder ".Take(3)"
            // för att hämta de 3 första korten från den slumpade listan.
            var threeCards = await _context.TarotCards
                                           .Include(c => c.Meanings)
                                               .ThenInclude(m => m.MeaningCategory)
                                           .OrderBy(c => EF.Functions.Random())
                                           .Take(3)
                                           .ToListAsync(); // Notera: ToListAsync() istället för FirstOrDefault!

            // Vi skickar en LISTA med 3 kort till vyn
            return View(threeCards);
        }
    }
}
