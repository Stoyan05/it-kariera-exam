using BarRating.Models;
using BusinessLayer.Services;
using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace BarRating.Controllers
{
    public class BarController : Controller
    {
        private readonly BarServices _barServices;
        private readonly IWebHostEnvironment _webHost;
        public BarController(BarServices barServices, IWebHostEnvironment webHost)
        {
            _barServices = barServices;
            _webHost = webHost;
        }

        private const string barsList = "~/Views/Bar/Index.cshtml";
        private const string adminsBarsList = "~/Views/Bar/ListAll.cshtml";
        private const string create = "~/Views/Bar/Create.cshtml";
        private const string edit = "~/Views/Bar/Edit.cshtml";
        private const string delete = "~/Views/Bar/Delete.cshtml";
        private const string SearchPath = "~/Views/Bar/Search.cshtml";
        // GET: BarController
        public async Task<ActionResult> Index([Bind("UserId")] string userId, int page = 1)
        {
            try
            {
                var barsPerPage = 4.0;
                var bars = await _barServices.ReadAllAsync();
                var paginatedBars = bars.Skip((page - 1) * (int)barsPerPage).Take((int)barsPerPage).ToList();
                ViewBag.Pages = Math.Ceiling((double)(bars.Count / barsPerPage));
                ViewBag.CurrentPage = page;
                foreach (var bar in bars)
                {
                    Console.WriteLine(bar.ToString());
                }
                Console.WriteLine("AZ SUM TUKAAA" + userId);
                return View(barsList, new BarModel { Bars = paginatedBars, UserId = userId });

            }
            catch (Exception ex)
            {
                // Log the exception or handle it appropriately
                return View("Error");
            }
        }

        // GET: BarController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Bar bar = await _barServices.ReadAsync(id);
            return View(bar);
        }

        // GET: BarController/Create
        public ActionResult Create()
        {
            return View(create);
        }

        // POST: BarController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        
            public async Task<ActionResult> Create([Bind("Name, Description")] string name, string description, IFormFile imgfile)
            {
                if (imgfile != null && imgfile.Length > 0)
                {
                    // Process the uploaded image
                    string uniqueFileName = Path.GetRandomFileName() + Path.GetExtension(imgfile.FileName); // Unique filename
                    string imagePath = "/Images/" + uniqueFileName; // Path where the image will be saved

                    // Save the image to wwwroot/Images folder
                    string uploadFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images");
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await imgfile.CopyToAsync(stream);
                    }

                    // Now create your Bar entity with the image path
                    Bar bar = new Bar(name, description, uniqueFileName);
                    await _barServices.CreateAsync(bar);
                    var bars = await _barServices.ReadAllAsync();
                    return View(adminsBarsList, new AdminModel { Bars = bars });
                }
                else
                {
                    // Handle case where no image is uploaded
                    // You may want to show an error message or take other action
                    return View();
                }
            }

               
           
        

        public async Task<ActionResult> Edit(int id)
        {
            Bar bar = await _barServices.ReadAsync(id);
            return View(bar);
        }

        // POST: BarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("BarId,Name, Description, Image")] int barId, string name, string description, string? image, bool shutup)
        {
            try
            {
                Bar bar = await _barServices.ReadAsync(barId);
                bar.Name = name;
                bar.Description = description;
                bar.Image = image;
                await _barServices.UpdateAsync(bar);
                return RedirectToAction("ListAll", new AdminModel { Bars = new List<Bar>() { bar} });
            }
            catch
            {
                return View(edit);
            }
        }
        // GET: BarController/Delete/5
        public async Task<ActionResult> Delete(int id, string title, string author, int year)
        {
            Bar bar = await _barServices.ReadAsync(id);
            return View(bar);
        }

        // POST: BarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _barServices.DeleteAsync(id);
                return RedirectToAction("ListAll");
            }
            catch
            {
                return View(delete);
            }
        }
        public async Task<ActionResult> ListAll()
        {
            List<Bar> bars = await _barServices.ReadAllAsync();
            return View(adminsBarsList, new AdminModel { Bars = bars });
        }
        public async Task<IActionResult> Search(BarModel model, string searchString)
        {


            var bars = await _barServices.SearchHotels(searchString);

            return View(SearchPath, new BarModel { Bars = bars });

        }

    }
}
