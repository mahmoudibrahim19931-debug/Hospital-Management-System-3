using Hospital.Models;
using Hospital.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hospital.Utilities
{
    public class DbInitializer : IDbInitializer
    {

        private UserManager<ApplicationUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private ApplicationDbContext _context;

        public DbInitializer(
    UserManager<ApplicationUser> userManager,
    RoleManager<IdentityRole> roleManager,
    ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception)
            {
                throw;
            }

            if (!_roleManager.RoleExistsAsync(WebSiteRoles.WebSite_Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(
                    new IdentityRole(WebSiteRoles.WebSite_Admin)
                ).GetAwaiter().GetResult();

                _roleManager.CreateAsync(
                    new IdentityRole(WebSiteRoles.WebSite_Patient)
                ).GetAwaiter().GetResult();

                _roleManager.CreateAsync(
                    new IdentityRole(WebSiteRoles.WebSite_Doctor)
                ).GetAwaiter().GetResult();

                _userManager.CreateAsync(
                    new ApplicationUser {
                    UserName = "Mahmoud",
                    Email = "mahmoud@gmail.com"


                    }, "Password123!").GetAwaiter().GetResult();

                

                var Appuser = _context.ApplicationUsers
    .FirstOrDefault(x => x.Email == "mahmoud@gmail.com");

                if (Appuser != null)
                {
                    _userManager.AddToRoleAsync(
                        Appuser,
                        WebSiteRoles.WebSite_Admin
                    ).GetAwaiter().GetResult();
                }

            }
        }
    }
}
