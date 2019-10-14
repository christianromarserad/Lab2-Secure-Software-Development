// I, Christian Serad, student number 000395777, certify that this material is my
// original work. No other person's work has been used without due
// acknowledgement and I have not made my work available to anyone else.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lab1BVersion2.Data;
using Lab1BVersion2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lab1BVersion2.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> RoleManager)
        {
            _userManager = userManager;
            _roleManager = RoleManager;
        }

        public async Task<IActionResult> Seed()
        {
            ApplicationUser user1 = new ApplicationUser
            {
                BirthDate = DateTime.Now,
                Company = "Mohawk College",
                Email = "johndoe@mohawkcollege.ca",
                UserName = "johndoe@mohawkcollege.ca",
                FirstName = "John",
                LastName = "Doe",
                Position = "Employee"
            };

            ApplicationUser user2 = new ApplicationUser
            {
                BirthDate = DateTime.Now,
                Company = "Mohawk College",
                Email = "janedoe@mohawkcollege.ca",
                UserName = "janedoe@mohawkcollege.ca",
                FirstName = "Jane",
                LastName = "Doe",
                Position = "Supervisor"
            };


            IdentityResult result = await _userManager.CreateAsync(user1, "@Mohawk1");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _userManager.CreateAsync(user2, "@Mohawk1");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new user" });

            result = await _roleManager.CreateAsync(new IdentityRole("Employee"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });

            result = await _roleManager.CreateAsync(new IdentityRole("Supervisor"));
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to add new role" });


            result = await _userManager.AddToRoleAsync(user1, "Employee");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });

            result = await _userManager.AddToRoleAsync(user2, "Supervisor");
            if (!result.Succeeded)
                return View("Error", new ErrorViewModel { RequestId = "Failed to assign new role" });


            return RedirectToAction("Index", "Home");
        }
    }
}