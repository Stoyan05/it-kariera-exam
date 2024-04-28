using DataLayer.Data.Entities;

namespace BarRating.Models
{
    public class BarModel
    {
        public List<Bar> Bars { get; set; }
        public int BarId { get; set; }
        public string UserId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }


    }
}
