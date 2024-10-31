// Controllers/HomeController.cs
using System;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;

namespace Aetherworks_Victuz.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(30); // Next 31 days including today

            var activities = _context.VictuzActivities
                .Where(a => a.ActivityDate.Date >= startDate && a.ActivityDate.Date <= endDate)
                .ToList();

            var model = new CalendarViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Activities = activities
            };

            return View(model);
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
