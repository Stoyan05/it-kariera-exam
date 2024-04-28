using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public User()
        {
            
        }
        public User(string username, string firstName, string lastName)
        {
            this.UserName = username;
            this.NormalizedUserName = username.ToUpper();
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
