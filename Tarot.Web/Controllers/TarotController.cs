using Microsoft.AspNetCore.Mvc;
using Tarot.Data;
using Microsoft.EntityFrameworkCore;
using Tarot.Web.Models;
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

        public async Task<IActionResult> DrawToday()
        {
            // 1. Hämta ett slumpmässigt kort
            var randomCard = await _context.TarotCards
                                           .Include(c => c.Meanings)
                                               .ThenInclude(m => m.MeaningCategory)
                                           .OrderBy(c => EF.Functions.Random())
                                           .FirstOrDefaultAsync();

            if (randomCard == null)
            {
                return NotFound("Kunde inte hitta några kort i leken!");
            }

            // 2. NU: Singla slant om det ska vara omvänt!
            var random = new Random();
            bool isReversed = random.Next(2) == 0; // 50% chans

            // 3. Slå in paketet i vår ViewModel
            var drawnCard = new DrawnCard
            {
                Card = randomCard,
                IsReversed = isReversed
            };

            // 4. Skicka paketet (DrawnCard) till vyn
            return View(drawnCard);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // Hämta kortet med matchande ID + dess betydelser
            var card = await _context.TarotCards
                .Include(c => c.Meanings)
                    .ThenInclude(m => m.MeaningCategory)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (card == null)
            {
                return NotFound();
            }

            return View(card);
        }

        public async Task<IActionResult> ThreeCardSpread()
        {
            // 1. Hämta 3 slumpmässiga kort från databasen
            var cardsFromDb = await _context.TarotCards
                                            .Include(c => c.Meanings)
                                                .ThenInclude(m => m.MeaningCategory)
                                            .OrderBy(c => EF.Functions.Random())
                                            .Take(3)
                                            .ToListAsync();

            // 2. Skapa vår lista med "Omslagspapper" (ViewModels)
            var drawnCards = new List<DrawnCard>();
            var random = new Random();

            foreach (var card in cardsFromDb)
            {
                // Singla slant! (0 eller 1)
                // Om det blir 0 så är IsReversed = true (omvänt)
                bool upsideDown = random.Next(2) == 0;

                drawnCards.Add(new DrawnCard
                {
                    Card = card,
                    IsReversed = upsideDown
                });
            }

            // 3. Skicka listan med DRAWNCARDS (inte TarotCards) till vyn
            return View(drawnCards);
        }
        public async Task<IActionResult> CelticCross()
        {
            // 1. Hämta 10 slumpmässiga kort
            var cardsFromDb = await _context.TarotCards
                                            .Include(c => c.Meanings)
                                                .ThenInclude(m => m.MeaningCategory)
                                            .OrderBy(c => EF.Functions.Random())
                                            .Take(10) // <--- TIO KORT!
                                            .ToListAsync();

            // 2. Skapa ViewModels (hantera omvända kort)
            var drawnCards = new List<DrawnCard>();
            var random = new Random();

            foreach (var card in cardsFromDb)
            {
                bool isReversed = random.Next(2) == 0;
                drawnCards.Add(new DrawnCard
                {
                    Card = card,
                    IsReversed = isReversed
                });
            }

            return View(drawnCards);
        }
    }
}
