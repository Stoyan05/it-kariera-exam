using BarRating.Models;
using BusinessLayer.Services;
using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Differencing;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using BarRating.Models;

namespace BarRating.Controllers
{
    public class UserController : Controller
    {
        // GET: UserController
        private readonly UserServices _services;

        private const string usersLink = "~/Views/User/ListAll.cshtml";
        private const string usersEditLink = "~/Views/User/Edit.cshtml";
        //private const string registration 
        public UserController(UserServices services)
        {
            _services = services;
        }
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public async Task<ActionResult> Details(string id)
        {
            User user = await _services.ReadUserAsync(id);
            return View(new UsersModel { FirstName = user.FirstName, LastName = user.LastName, UserName = user.UserName});
        }

        public ActionResult Create()
        {
            return Redirect("/Identity/Account/Register");
        }

        // POST: BarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        //public async Task<ActionResult> Create([Bind("FirstName, LastName, UserName")] string firstName, string lastName, string? username, )
        //{
        //    try
        //    {
        //        Bar bar = new Bar(name, description, image);
        //        await _barServices.CreateAsync(bar);
        //        return RedirectToAction("ListAll");
        //    }
        //    catch
        //    {
        //        return View(adminsBarsList);
        //    }
        //}


        // GET: UserController/Edit/5
        [HttpGet]
        public async Task<ActionResult> Edit(string id)
        {

            User user = await _services.ReadUserAsync(id);
            Console.WriteLine("MOETO ID" + user.Id);
            return View(usersEditLink, new UsersModel { Id = user.Id, FirstName = user.FirstName, LastName = user.LastName, UserName = user.UserName });
        }

        // POST: UserController/Edit/5
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost([Bind("Id,FirstName, LastName, UserName")] string id, string firstName, string lastName, string username, bool shutup)
        {
            try
            {
                User user = await _services.ReadUserAsync(id);
                await _services.UpdateAccountAsync(firstName, lastName, username);
                return RedirectToAction("ListAll", new UsersModel { Users = new List<User>() { user } });
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            User user = await _services.ReadUserAsync(id);
            return View(user);
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id, bool shutup)
        {
           Console.WriteLine("TUKA SUM WEEE" + id);
                await _services.DeleteAccountAsync(id);
                return RedirectToAction("ListAll");
          
        }
        public async Task<ActionResult> ListAll()
        {
            var users = await _services.ReadAllUsersAsync();
            return View(usersLink, new UsersModel { Users = users });
        }
    }
}
