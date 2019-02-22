using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Assignment1.Models
{
    public class Person
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "Enter the user name")]
        public string user_id { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "Enter any complex password")]
        public string password { get; set; }
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Enter your full name")]
        public string fullName { get; set; }
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [Required(ErrorMessage = "Email is a required field")]
        public string email { get; set; }
        [Display(Name = "Joined On")]
        public DateTime joined { get; set; }
        public Boolean active { get; set; }
        
    }
}
