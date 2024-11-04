// Controllers/VictuzActivitiesController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;
using static Aetherworks_Victuz.Models.VictuzActivity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;

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
            // Define the date range for the calendar
            var startDate = DateTime.Today;
            var endDate = startDate.AddDays(30); // Next 31 days including today

            // Retrieve activities within the date range and include related entities
            var activities = await _context.VictuzActivities
                .Where(a => a.ActivityDate.Date >= startDate && a.ActivityDate.Date <= endDate)
                .OrderBy(a => a.ActivityDate)
                .Include(v => v.Host)
                .Include(v => v.Location)
                .Include(v => v.ParticipantsList)
                .ToListAsync();

            // Create the CalendarViewModel
            var calendarViewModel = new CalendarViewModel
            {
                StartDate = startDate,
                EndDate = endDate,
                Activities = activities
            };

            // Create the CompositeViewModel
            var viewModel = new CompositeViewModel
            {
                Calendar = calendarViewModel,
                Activities = activities // Add activities to the composite model
            };

            // Pass the composite view model to the view
            return View(viewModel);
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

            var viewModel = new VictuzActivityViewModel() 
            { 
                VictuzActivity = victuzActivity, 
                Attendees = attendees.ToList() 
            };
            viewModel.SetOldPicture();



            return View(viewModel);
        }

        public string GetDisplayNameForCategory(ActivityCategories category)
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

        // GET: VictuzActivities/Create
        public IActionResult Create()
        {
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id");
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Id");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            return View();
        }

        // POST: VictuzActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Name,Description,LocationId,ActivityDate,HostId,Price,MemberPrice,ParticipantLimit")] VictuzActivity victuzActivity, IFormFile? PictureFile)
        {
            if (ModelState.IsValid)
            {
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

                // Add the activity to the database and save changes
                _context.Add(victuzActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id", victuzActivity.HostId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", victuzActivity.LocationId);
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            return View(victuzActivity);
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
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id", victuzActivity.HostId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", victuzActivity.LocationId);
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            ViewData["CurrentCategory"] = GetDisplayNameForCategory(victuzActivity.Category);
            return View(victuzActivity);
        }

        // POST: VictuzActivities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Name,Description,LocationId,ActivityDate,HostId,Price,MemberPrice,ParticipantLimit,Picture")] VictuzActivity victuzActivity, IFormFile? PictureFile)
        {
            if (id != victuzActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (PictureFile != null && PictureFile.Length > 0)
                {
                    string imgFolderPath = Path.Combine(_webHostEnvironment.WebRootPath, "img");

                    if (!Directory.Exists(imgFolderPath))
                    {
                        Directory.CreateDirectory(imgFolderPath);
                    }
                    else
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

            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id", victuzActivity.HostId);
            ViewData["LocationId"] = new SelectList(_context.Locations, "Id", "Id", victuzActivity.LocationId);
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");

            return View(victuzActivity);
        }

        // GET: VictuzActivities/Delete/5
        public async Task<IActionResult> Delete(int? id)
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

            return View(victuzActivity);
        }

        // POST: VictuzActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var victuzActivity = await _context.VictuzActivities.FindAsync(id);
            if (victuzActivity != null)
            {
                _context.VictuzActivities.Remove(victuzActivity);
            }
            await _context.SaveChangesAsync();
            if (!string.IsNullOrEmpty(victuzActivity.Picture))
            {
                var oldFilePath = Path.Combine(_webHostEnvironment.WebRootPath, victuzActivity.Picture.TrimStart('\\'));
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        private bool VictuzActivityExists(int id)
        {
            return _context.VictuzActivities.Any(e => e.Id == id);
        }

        // Optional: If you want to display activities by date
        public async Task<IActionResult> ActivitiesByDate(string date)
        {
            if (string.IsNullOrEmpty(date))
            {
                return NotFound();
            }

            DateTime selectedDate;
            if (!DateTime.TryParse(date, out selectedDate))
            {
                return NotFound();
            }

            var activities = await _context.VictuzActivities
                .Where(a => a.ActivityDate.Date == selectedDate.Date)
                .ToListAsync();

            return View(activities);
        }
    }
}
