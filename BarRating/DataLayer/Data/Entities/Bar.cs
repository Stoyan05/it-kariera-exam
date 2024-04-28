using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data.Entities
{
    public class Bar
    {
        [Key]
        public int BarId { get; set; }
        [Required]
        [StringLength(64, ErrorMessage = "Name Max Length is 64")]
        public string Name { get; set; }
        [Required]
        [StringLength(255, ErrorMessage = "Description Max Length is 255")]
        public string Description { get; set; } 
        public string? Image { get; set; }
        
      
        public Bar()
        {
            
        }
        public Bar(string name, string description, string? image)
        {
            this.Name = name;
            this.Description = description;
            this.Image = image;
        }
    }
}
