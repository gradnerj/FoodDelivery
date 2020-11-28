using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.Data;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.User
{
    public class UpdateModel : PageModel
    {
        private ApplicationDbContext _context;
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public UpdateModel(ApplicationDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [BindProperty]
        public ApplicationUser AppUser { get; set; }
        
        public List<string> UsersRoles { get; set; }
        public List<string> AllRoles { get; set; }

        public List<string> OldRoles { get; set; }
        public async Task OnGetAsync(string id)
        {
            AppUser = _context.ApplicationUser.FirstOrDefault(u => u.Id == id);
            var roles = await _userManager.GetRolesAsync(AppUser);
            UsersRoles = roles.ToList();
            OldRoles = roles.ToList();
            AllRoles = _roleManager.Roles.Select(r => r.Name).ToList();
        }
        public async Task<IActionResult> OnPostAsync() {
            var newRoles = Request.Form["roles"];
            UsersRoles = newRoles.ToList();
            var oldRoles = await _userManager.GetRolesAsync(AppUser);
            OldRoles = oldRoles.ToList();
            var rolesToAdd = new List<string>();
            var user = _context.ApplicationUser.FirstOrDefault(u => u.Id == AppUser.Id);
            user.FirstName = AppUser.FirstName;
            user.LastName = AppUser.LastName;
            user.Email = AppUser.Email;
            user.PhoneNumber = AppUser.PhoneNumber;
            _context.ApplicationUser.Update(user);
            _context.SaveChanges();
            foreach(var r in UsersRoles) {
                if (!OldRoles.Contains(r)) {
                    rolesToAdd.Add(r);
                }
            }
            foreach(var r in OldRoles) {
                if (!UsersRoles.Contains(r)) {
                    var result = await _userManager.RemoveFromRoleAsync(user, r);
                }
            }
            var result1 = await _userManager.AddToRolesAsync(user, rolesToAdd.AsEnumerable());
            return RedirectToPage(new { id = AppUser.Id });
        }
    }
}