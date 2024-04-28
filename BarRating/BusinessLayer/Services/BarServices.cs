using DataLayer;
using DataLayer.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BarServices : IDbCRUD<Bar, int>
    {
        private readonly BarDbContext _context; //DB context
        public BarServices(BarDbContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Bar bar)
        {
            try
            {
                _context.Bars.Add(bar);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Bar was not added!");
            }
        }
        public async Task<List<Bar>> ReadAllAsync(bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Bar> bars = _context.Bars;
                if (isReadOnly)
                {
                    bars.AsNoTrackingWithIdentityResolution();
                }
                return await bars.ToListAsync();
            }
            catch
            {
                throw new Exception("There are no bars!");
            }
        }

        public async Task<Bar> ReadAsync(int key, bool useNavigationalProperties = false, bool isReadOnly = true)
        {
            try
            {
                IQueryable<Bar> bars = _context.Bars;
                if (isReadOnly)
                {
                    bars.AsNoTrackingWithIdentityResolution();
                }
                return await bars.SingleOrDefaultAsync(x => x.BarId == key);
            }
            catch
            {
                throw new Exception("Bar was not found!");
            }
        }
        public async Task UpdateAsync(Bar bar)
        {
            try
            {
                _context.Bars.Update(bar);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Bar could not be updated!");
            }
        }

        public async Task DeleteAsync(int key)
        {
            try
            {
                Bar bar = await ReadAsync(key);
                if (bar is null)
                {
                    throw new ArgumentException(string.Format($"Bar with id {key} does not exist in the database!"));
                }
                _context.Bars.Remove(bar);
                await _context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Bar could not be deleted!");
            }
        }

        public async Task<List<Bar>> SearchHotels(string searchString)
        {
            // Ensure searchString is not null before calling ToLower to avoid null reference exceptions   
            List<Bar> bars = new List<Bar>();
            searchString = searchString?.ToLower();

            bars = await _context.Bars.Where(x => searchString == null || x.Name.ToLower().StartsWith(searchString)).ToListAsync();

            return bars;
        }
    }
}
