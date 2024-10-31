// Controllers/VictuzActivitiesController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Aetherworks_Victuz.Data;
using Aetherworks_Victuz.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aetherworks_Victuz.Controllers
{
    public class VictuzActivitiesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VictuzActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: VictuzActivities
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.VictuzActivities.Include(v => v.Host).Include(v => v.Location);
            return View(await applicationDbContext.ToListAsync());
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

            return View(victuzActivity);
        }

        private string GetDisplayNameForCategory(VictuzActivity.ActivityCategories category)
        {
            return category switch
            {
                VictuzActivity.ActivityCategories.Free => "Free for Everyone",
                VictuzActivity.ActivityCategories.MemFree => "Free for Members Only",
                VictuzActivity.ActivityCategories.PayAll => "Paid for All",
                VictuzActivity.ActivityCategories.MemOnlyFree => "Members Only - Free",
                VictuzActivity.ActivityCategories.MemOnlyPay => "Members Only - Paid",
                _ => category.ToString()
            };
        }

        // GET: VictuzActivities/Create
        public IActionResult Create()
        {
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id");
            var enumCategories = Enum.GetValues(typeof(VictuzActivity.ActivityCategories))
                .Cast<VictuzActivity.ActivityCategories>()
                .ToDictionary(
                    category => category,
                    category => GetDisplayNameForCategory(category)
                );
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Id");
            ViewData["Category"] = new SelectList(enumCategories, "Key", "Value");
            return View();
        }

        // POST: VictuzActivities/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Category,Name,Description,LocationId,ActivityDate,HostId,Price,MemberPrice,ParticipantLimit")] VictuzActivity victuzActivity)
        {
            Console.WriteLine(victuzActivity);
            if (ModelState.IsValid)
            {
                _context.Add(victuzActivity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id", victuzActivity.HostId);
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Id", victuzActivity.LocationId);
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
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Id", victuzActivity.LocationId);
            return View(victuzActivity);
        }

        // POST: VictuzActivities/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Category,Name,Description,LocationId,ActivityDate,HostId,Price,MemberPrice,ParticipantLimit")] VictuzActivity victuzActivity)
        {
            if (id != victuzActivity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(victuzActivity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VictuzActivityExists(victuzActivity.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["HostId"] = new SelectList(_context.User, "Id", "Id", victuzActivity.HostId);
            ViewData["LocationId"] = new SelectList(_context.Set<Location>(), "Id", "Id", victuzActivity.LocationId);
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
