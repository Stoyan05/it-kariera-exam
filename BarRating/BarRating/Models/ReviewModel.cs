using DataLayer.Data.Entities;

namespace BarRating.Models
{
    public class ReviewModel
    {
        public List<Review> Reviews { get; set; }
        public int ReviewId { get; set; }
        public string UserId { get; set; }
        public int BarId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public string BarName { get; set; }
    }
}
