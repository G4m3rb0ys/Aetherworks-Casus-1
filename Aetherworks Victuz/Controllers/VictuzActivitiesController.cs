using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;
using System.Diagnostics;
using static Aetherworks_Victuz.Models.VictuzActivity;

namespace Aetherworks_Victuz.Controllers
{
    public class VictuzActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public VictuzActivitiesController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: VictuzActivities
        public async Task<IActionResult> Index()
        {
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(30);

            var activities = await _context.VictuzActivities
                .Where(a => a.ActivityDate.Date >= startDate && a.ActivityDate.Date <= endDate)
                .OrderBy(a => a.ActivityDate)
                .Include(v => v.Host)
                .Include(v => v.Location)
                .Include(v => v.ParticipantsList)
                .ToListAsync();

            var calendarViewModel = new CalendarViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Activities = activities
            };

            var viewModel = new CompositeViewModel
            {
                Calendar = calendarViewModel,
                Activities = activities
            };

            return View(viewModel);
        }

        // POST: VictuzActivities/Register
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Register(int activityId)
        {
            var activity = await _context.VictuzActivities
                .Include(a => a.ParticipantsList)
                .FirstOrDefaultAsync(a => a.Id == activityId);

            if (activity == null)
            {
                return NotFound();
            }

            if (activity.ParticipantLimit > 0 && activity.ParticipantsList.Count >= activity.ParticipantLimit)
            {
                TempData["ErrorMessage"] = "De activiteit zit vol.";
                return RedirectToAction("Index");
            }

            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identityUserId))
            {
                TempData["ErrorMessage"] = "Kan gebruikers-ID niet ophalen. Controleer je loginstatus.";
                return RedirectToAction("Index");
            }

            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Credential != null && u.Credential.Id == identityUserId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Gebruiker niet gevonden.";
                return RedirectToAction("Index");
            }

            if (activity.ParticipantsList.Any(p => p.UserId == user.Id))
            {
                TempData["ErrorMessage"] = "Je bent al ingeschreven voor deze activiteit.";
                return RedirectToAction("Index");
            }

            var participation = new Participation
            {
                UserId = user.Id
            };

            activity.ParticipantsList.Add(participation);
            _context.Update(activity);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Je bent succesvol ingeschreven voor de activiteit.";
            return RedirectToAction("Index");
        }

        // GET: VictuzActivities/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var victuzActivity = await _context.VictuzActivities
                .Include(v => v.Host)
                .Include(v => v.Location)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (victuzActivity == null)
            {
                return NotFound();
            }

            var attendees = _context.Participation
                .Include(p => p.User)
                .ThenInclude(u => u.Credential)
                .Where(p => p.ActivityId == id);

            var viewModel = new VictuzActivityViewModel
            {
                VictuzActivity = victuzActivity,
                Attendees = attendees.ToList(),
                Locations = await _context.Locations.ToListAsync(),
                Hosts = await _context.User.Include(u => u.Credential).ToListAsync()
            };

            return View(viewModel);
        }

        // GET: VictuzActivities/Create
        public IActionResult Create()
        {
            ViewData["HostId"] = new SelectList(_context.User.Include(u => u.Credential), "Id", "Credential.UserName");
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Name");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            var viewModel = new VictuzActivityViewModel
            {
                Locations = _context.Locations.ToList(),
                Hosts = _context.User.Include(u => u.Credential).Where(u => u.Credential != null).ToList()

            };

            return View(viewModel);
        }

        // POST: VictuzActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(VictuzActivityViewModel viewModel, IFormFile? PictureFile)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState)
                {
                    Debug.WriteLine($"{error.Key}: {string.Join(", ", error.Value.Errors.Select(e => e.ErrorMessage))}");
                }
                // Return to the view with errors to display to the user
                return View(viewModel);
            }

            if (ModelState.IsValid)
            {
                var victuzActivity = viewModel.VictuzActivity;

                if (PictureFile != null && PictureFile.Length > 0)
                {
                    string imgFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                    if (!Directory.Exists(imgFolderPath))
                    {
                        Directory.CreateDirectory(imgFolderPath);
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(PictureFile.FileName);
                    string filePath = Path.Combine(imgFolderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PictureFile.CopyToAsync(stream);
                    }

                    victuzActivity.Picture = "\\img\\" + uniqueFileName;
                }

                _context.Add(victuzActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            viewModel.Locations = _context.Locations.ToList();
            viewModel.Hosts = _context.User.ToList();

            ViewData["HostId"] = new SelectList(_context.User.Include(u => u.Credential), "Id", "Credential.UserName");
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Name");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            return View(viewModel);
        }

        // GET: VictuzActivities/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var victuzActivity = await _context.VictuzActivities.FindAsync(id);
            if (victuzActivity == null)
            {
                return NotFound();
            }

            var viewModel = new VictuzActivityViewModel
            {
                VictuzActivity = victuzActivity,
                Locations = await _context.Locations.ToListAsync(),
                Hosts = await _context.User.ToListAsync()
            };

            ViewData["HostId"] = new SelectList(_context.User.Include(u => u.Credential), "Id", "Credential.UserName");
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Name");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");

            return View(viewModel);
        }

        // POST: VictuzActivities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, VictuzActivityViewModel viewModel, IFormFile? PictureFile)
        {
            if (id != viewModel.VictuzActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var victuzActivity = viewModel.VictuzActivity;

                if (PictureFile != null && PictureFile.Length > 0)
                {
                    string imgFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                    if (!Directory.Exists(imgFolderPath))
                    {
                        Directory.CreateDirectory(imgFolderPath);
                    }
                    else if (!string.IsNullOrEmpty(victuzActivity.Picture))
                    {
                        string fullImagePath = Path.Combine(_webHostEnvironment.WebRootPath, victuzActivity.Picture.TrimStart('\\'));
                        if (System.IO.File.Exists(fullImagePath))
                        {
                            System.IO.File.Delete(fullImagePath);
                        }
                    }

                    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(PictureFile.FileName);
                    string filePath = Path.Combine(imgFolderPath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await PictureFile.CopyToAsync(stream);
                    }

                    victuzActivity.Picture = "\\img\\" + uniqueFileName;
                }

                _context.Update(victuzActivity);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            ViewData["HostId"] = new SelectList(_context.User.Include(u => u.Credential), "Id", "Credential.UserName");
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Name");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");

            viewModel.Locations = _context.Locations.ToList();
            viewModel.Hosts = _context.User.ToList();
            return View(viewModel);
        }

        // Additional methods (Delete, Reservations, etc.) remain unchanged

        private bool VictuzActivityExists(int id)
        {
            return _context.VictuzActivities.Any(e => e.Id == id);
        }

        private string GetDisplayNameForCategory(ActivityCategories category)
        {
            return category switch
            {
                ActivityCategories.Free => "Free Activity",
                ActivityCategories.MemFree => "Free for Members",
                ActivityCategories.PayAll => "Paid for All",
                ActivityCategories.MemOnlyFree => "Members Only - Free",
                ActivityCategories.MemOnlyPay => "Members Only - Paid",
                _ => category.ToString()
            };
        }

        [HttpPost]
        public IActionResult ToggleAttendance(int participationId)
        {
            var participation = _context.Participation.FirstOrDefault(p => p.Id == participationId);
            if (participation == null)
            {
                return NotFound();
            }

            participation.Attended = !participation.Attended;
            _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = participation.ActivityId });
        }

        public async Task<IActionResult> Reservations()
        {
            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(identityUserId))
            {
                TempData["ErrorMessage"] = "Kan gebruikers-ID niet ophalen. Controleer je loginstatus.";
                return RedirectToAction("Index");
            }
            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Credential != null && u.Credential.Id == identityUserId);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Gebruiker niet gevonden.";
                return RedirectToAction("Index");
            }
            var reservations = _context.Participation
                .Include(p => p.Activity)
                .ThenInclude(a => a.Host)
                .Include(p => p.Activity)
                .ThenInclude(a => a.Location)
                .Where(p => p.UserId == user.Id);
            return View(reservations);
        }

        // POST: VictuzActivities/Unregister
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Unregister(int activityId)
        {
            var activity = await _context.VictuzActivities
                .Include(a => a.ParticipantsList)
                .FirstOrDefaultAsync(a => a.Id == activityId);

            if (activity == null)
            {
                return NotFound();
            }

            var identityUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(identityUserId))
            {
                TempData["ErrorMessage"] = "Kan gebruikers-ID niet ophalen. Controleer je loginstatus.";
                return RedirectToAction("Index");
            }

            var user = await _context.User
                .FirstOrDefaultAsync(u => u.Credential != null && u.Credential.Id == identityUserId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Gebruiker niet gevonden.";
                return RedirectToAction("Index");
            }

            var participation = activity.ParticipantsList.FirstOrDefault(p => p.UserId == user.Id);
            if (participation == null)
            {
                TempData["ErrorMessage"] = "Je bent niet ingeschreven voor deze activiteit.";
                return RedirectToAction("Index");
            }

            activity.ParticipantsList.Remove(participation);
            _context.Update(activity);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Je bent succesvol uitgeschreven voor de activiteit.";
            return RedirectToAction("Index");
        }
    }

}
