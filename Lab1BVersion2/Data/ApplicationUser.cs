using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Lab1BVersion2.Data
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Company { get; set; }
        public string Position { get; set; }
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}
