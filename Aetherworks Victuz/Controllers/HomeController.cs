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
        private readonly UserManager<IdentityUser> _userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
        }

        // Index Actie
        public async Task<IActionResult> Index()
        {
            var identityUserId = _userManager.GetUserId(User);
            var user = await _context.User.FirstOrDefaultAsync(u => u.CredentialId == identityUserId);

            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(30); // Volgende 31 dagen inclusief vandaag

            // Fetch recent activities and suggestions
            var activities = await _context.VictuzActivities
                .Where(a => a.ActivityDate >= startDate && a.ActivityDate <= endDate)
                .OrderBy(a => a.ActivityDate)
                .Include(a => a.ParticipantsList)
                .ToListAsync();

            var suggestions = await _context.Suggestions
                .Include(s => s.SuggestionLikeds)
                .OrderByDescending(s => s.Id)
                .Take(3)
                .Take(3)
                .Select(s => new SuggestionViewModel
                {
                    Suggestion = s,
                    LikeCount = s.SuggestionLikeds.Count,
                    IsLiked = s.SuggestionLikeds.Any(sl => sl.UserId == user.Id)
                })
                .ToListAsync();

            var model = new HomeViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Activities = activities,
                Suggestions = suggestions
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

        [HttpPost]
        public async Task<IActionResult> ToggleLike(int suggestionId)
        {
            // Get the current logged-in user's ID
            var identityUserId = _userManager.GetUserId(User);
            var user = await _context.User.FirstOrDefaultAsync(u => u.CredentialId == identityUserId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Check if the user has already liked the suggestion
            var existingLike = await _context.SuggestionLiked
                .FirstOrDefaultAsync(sl => sl.SuggestionId == suggestionId && sl.UserId == user.Id);

            if (existingLike != null)
            {
                // If a like exists, remove it (unlike)
                _context.SuggestionLiked.Remove(existingLike);
                await _context.SaveChangesAsync();
            }
            else
            {
                // If no like exists, add it (like)
                var newLike = new SuggestionLiked
                {
                    SuggestionId = suggestionId,
                    UserId = user.Id
                };

                _context.SuggestionLiked.Add(newLike);
                await _context.SaveChangesAsync();
            }

            // Calculate the updated like count after toggling
            var likeCount = await _context.SuggestionLiked.CountAsync(sl => sl.SuggestionId == suggestionId);

            // Return JSON with the updated like count
            return Json(new { success = true, liked = existingLike == null, likeCount });
        }
    }
}
