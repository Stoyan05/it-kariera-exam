using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data.Entities
{
    public class Review
    {
        [Key]
        public int ReviewId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        [ForeignKey("BarId")]
        public int BarId { get; set; }  
        public string Text { get; set; }
        public int Rating { get; set; }

        [AllowNull]
        [NotMapped]
        public User User { get; set; }
        [AllowNull]
        [NotMapped]
        public Bar Bar { get; set; }
        public Review()
        {
            
        }
        public Review(string userId, int barId, string text, int rating)
        {
            this.UserId = userId;
            this.BarId = barId;
            this.Text = text;
            this.Rating = rating;
        }
    }
}
