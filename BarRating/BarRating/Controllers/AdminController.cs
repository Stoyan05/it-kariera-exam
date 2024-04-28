using BarRating.Models;
using DataLayer;
using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BarRating.Controllers
{
    public class AdminController : Controller
    {
        private readonly BarDbContext _db;
       
        private const string admin = "~/Views/Admin/AdminStartPage.cshtml";
        public AdminController(BarDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _db.Users.ToListAsync();
            int userCount = users.Count;
            var reviews = await _db.Reviews.ToListAsync();
            int reviewsCount = reviews.Count;
            var bars = await _db.Bars.ToListAsync();
            var barsCount = bars.Count;
            return View(admin, new AdminModel { UsersCount = userCount , ReviewsCount = reviewsCount, BarsCount = barsCount});
        }
    }
}
