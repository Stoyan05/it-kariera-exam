using DataLayer.Data.Entities;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewServices : IDbCRUD<Review, int>
    {
        private readonly BarDbContext _context; //DB context
        public ReviewServices(BarDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Review review)
        {
            //try
            //{
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
            //}
            //catch
            //{
            //    throw new Exception("Review was not added!");
            //}
        }
        public async Task<List<Review>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Review> reviews = _context.Reviews;
                if (isReadOnly)
                {
                    reviews.AsNoTrackingWithIdentityResolution();
                }
                return await reviews.ToListAsync();
            }
            catch
            {
                throw new Exception("There are no reviews!");
            }
        }

        public async Task<Review> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Review> reviews = _context.Reviews;
                if (isReadOnly)
                {
                    reviews.AsNoTrackingWithIdentityResolution();
                }
                return await reviews.SingleOrDefaultAsync(x => x.ReviewId == key);
            }
            catch
            {
                throw new Exception("Review was not found!");
            }
        }
        public async Task UpdateAsync(Review review)
        {
            try
            {
                _context.Reviews.Update(review);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Reviews could not be updated!");
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Console.WriteLine("AZ SUM KLUCH" + key);
                Review review = await ReadAsync(key);
                if (review is null)
                {
                    throw new ArgumentException(string.Format($"Review with id {key} does not exist in the database!"));
                }
                _context.Reviews.Remove(review);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Review could not be deleted!");
            }
        }
        public async Task<List<Review>> ViewAllBarsReviews(int barId)
        {
           var result = _context.Reviews.Where(x => x.BarId == barId).ToList();
           return result;
            
        }
        public async Task<List<Review>> ViewAllUsersReviews(string userId)
        {
            var result = _context.Reviews.Where(x => x.UserId == userId).ToList();
            return result;

        }
    }
}