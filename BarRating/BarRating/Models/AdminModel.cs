using DataLayer.Data.Entities;

namespace BarRating.Models
{
    public class AdminModel
    {
        public int UsersCount { get; set; }
        public int ReviewsCount { get; set; }
        public int BarsCount { get; set; }
        public List<Bar> Bars { get; set; }
        public int BarId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
    }
}
