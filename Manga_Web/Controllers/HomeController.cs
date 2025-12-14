using Manga_Web.Models;
using MangaBook_Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Manga_Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MangaDbContext _context;

        public HomeController(ILogger<HomeController> logger, MangaDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var mangaRatings = _context.MangaBooks
                .OrderByDescending(m => m.AverageRating)
                .Take(10)
                .ToList();
            return View(mangaRatings);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
