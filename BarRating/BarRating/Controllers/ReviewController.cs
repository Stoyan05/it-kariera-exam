using BarRating.Models;
using BusinessLayer.Services;
using DataLayer.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;

namespace BarRating.Controllers
{
    public class ReviewController : Controller
    {
        private readonly ReviewServices _reviewServices;
        private readonly BarServices _barServices;
        public ReviewController(ReviewServices reviewServices, BarServices barServices)
        {
            _reviewServices = reviewServices;
            _barServices = barServices;
        }
        private const string createReview = "~/Views/Reviews/Create.cshtml";
        private const string details = "~/Views/Reviews/Details.cshtml";
        private const string edit = "~/Views/Reviews/Edit.cshtml";
       // private const string reviewList = "~/Views/Reviews/Edit.cshtml";
        // GET: ReviewController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReviewController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReviewController/Create
        //public ActionResult Create(int barId, string userId)
        //{
        //    return View(createReview); // Assuming you have a view for creating a review
        //}

        // POST: Review/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BarId, UserId, Text, Rating")] int barId, string userId, string text, int rating)
        {
            Console.WriteLine(barId);
            Console.WriteLine(userId);
                Review review = new Review(userId, barId, text, rating);
                await _reviewServices.CreateAsync(review);
                return RedirectToAction("Index", "Bar");
        }

        public async Task<ActionResult> Edit(string userId, int barId)
        {
            Console.WriteLine("BAR ID" + barId);
            Console.WriteLine("USER id" + userId);
            Bar bar = await _barServices.ReadAsync(barId);
            var barReviews = await _reviewServices.ViewAllBarsReviews(barId);
            barReviews = barReviews.Where(x => x.UserId == userId).ToList();
            foreach(var revies in barReviews)
            {
                Console.WriteLine(revies.Text);
            }
            return View(edit, new ReviewModel { Reviews = barReviews,  UserId = userId, BarId = barId});
        }

        // POST: BarController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind("UserId, BarId, ReviewId, Text, Rating")] int barId, string userId, int reviewId, string text, int rating)
        {
            Review review = await _reviewServices.ReadAsync(reviewId);
            review.Text = text;
            review.Rating = rating;
            await _reviewServices.UpdateAsync(review);
            return RedirectToAction("Index", "Bar", new {userId = userId});
        }

        // GET: ReviewController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Review review = await _reviewServices.ReadAsync(id);
            return View(review);
        }

        // POST: BarController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete([Bind("ReviewId")] int reviewId, bool shutup)
        {
            await _reviewServices.DeleteAsync(reviewId);
            Review review = await _reviewServices.ReadAsync(reviewId);
            return RedirectToAction("Index", "Bar");
        }
        public async Task<IActionResult> ViewAllReviews([Bind("BarId, UserId")] int barId, string userId)
        {
            Bar bar = await _barServices.ReadAsync(barId);
            var reviews = await _reviewServices.ViewAllBarsReviews(barId);
            return View(details, new ReviewModel { Reviews = reviews,  /*BarName = bar.Name,*/ BarId = barId, UserId = userId});
        }
    }
}
