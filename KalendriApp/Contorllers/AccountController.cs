using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using KalenderApp.Data;
using KalenderApp.Models;
using System.Threading.Tasks;

namespace KalenderApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Сохраняем UserId в сессии
                HttpContext.Session.SetInt32("UserId", user.Id);
                return RedirectToAction("Index", "Event");
            }

            // Если неверные учетные данные
            ViewBag.ErrorMessage = "Неверный email или пароль.";
            return View();
        }

        // GET: /Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(User newUser)
        {
            if (ModelState.IsValid)
            {
                // Проверка на наличие пользователя с таким же email
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == newUser.Email);
                if (existingUser == null)
                {
                    _context.Users.Add(newUser);
                    await _context.SaveChangesAsync();

                    // Сразу авторизуем пользователя после регистрации
                    HttpContext.Session.SetInt32("UserId", newUser.Id);
                    return RedirectToAction("Index", "Event");
                }
                else
                {
                    ViewBag.ErrorMessage = "Пользователь с таким email уже существует.";
                }
            }
            return View();
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Очищаем сессию
            return RedirectToAction("Login");
        }
    }
}
