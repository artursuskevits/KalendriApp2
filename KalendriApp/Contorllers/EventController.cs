// Controllers/EventController.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KalenderApp.Data;
using KalenderApp.Models;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace KalenderApp.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Event/Index
        public async Task<IActionResult> Index()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var events = await _context.Events
                .Include(e => e.Category)
                .Where(e => e.UserId == userId)
                .ToListAsync();

            return View(events);
        }

        // GET: /Event/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Event/Create
        [HttpPost]
        public async Task<IActionResult> Create(Event @event)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            @event.UserId = userId.Value;
            _context.Add(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /Event/Edit/{id}
        public async Task<IActionResult> Edit(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var @event = await _context.Events.FindAsync(id);
            if (@event == null || @event.UserId != userId)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: /Event/Edit
        [HttpPost]
        public async Task<IActionResult> Edit(Event @event)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            if (@event.UserId != userId)
            {
                return Unauthorized();
            }

            _context.Update(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: /Event/Delete/{id}
        public async Task<IActionResult> Delete(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var @event = await _context.Events.FindAsync(id);
            if (@event == null || @event.UserId != userId)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: /Event/DeleteConfirmed
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login", "Account");

            var @event = await _context.Events.FindAsync(id);
            if (@event != null && @event.UserId == userId)
            {
                _context.Events.Remove(@event);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}
