using BarRating.Models;
using BusinessLayer.Services;
using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BarRating.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly BarServices _barServices;

        public HomeController(ILogger<HomeController> logger, SignInManager<User> signInManager, UserManager<User> userManager, BarServices barServices)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
            _barServices = barServices;
        }
        private const string barsList = "~/Views/Bar/Index.cshtml";

        public async Task<IActionResult> Index()
        {
            return View();
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
