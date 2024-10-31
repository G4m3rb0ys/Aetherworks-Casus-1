// Controllers/HomeController.cs
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;
using Microsoft.EntityFrameworkCore; // Voor Include
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Aetherworks_Victuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        // Verwijderd _userManager omdat deze niet gebruikt wordt

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
            // Verwijderd _userManager
        }

        // Index Actie
        public IActionResult Index()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(30); // Volgende 31 dagen inclusief vandaag

            var activities = _context.VictuzActivities
                .Where(a => a.ActivityDate.Date >= startDate && a.ActivityDate.Date <= endDate)
                .OrderBy(a => a.ActivityDate) // Sorteer activiteiten op datum
                .Include(a => a.ParticipantsList)
                .ToList();

            // Haal de drie meest recente suggesties op
            var suggestions = _context.Suggestions
                .OrderByDescending(s => s.Id) // Sorteer op Id om de meest recente suggesties te krijgen
                .Take(3)
                .ToList();

            var model = new HomeViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Activities = activities,
                Suggestions = suggestions // Voeg de suggesties toe aan het model
            };

            return View(model);
        }

        // Privacy Actie
        public IActionResult Privacy()
        {
            return View();
        }

        // Contact Actie (als je deze hebt)
        public IActionResult Contact()
        {
            return View();
        }

        // Overzicht van alle activiteiten (optioneel)
        public IActionResult Activities()
        {
            var activities = _context.VictuzActivities
                .OrderBy(a => a.ActivityDate)
                .Include(a => a.ParticipantsList)
                .ToList();

            return View(activities);
        }

        // Details van een specifieke activiteit
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var activity = _context.VictuzActivities
                .Include(a => a.ParticipantsList)
                .FirstOrDefault(a => a.Id == id);

            if (activity == null)
            {
                return NotFound();
            }

            return View(activity);
        }

        // Foutafhandeling
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
