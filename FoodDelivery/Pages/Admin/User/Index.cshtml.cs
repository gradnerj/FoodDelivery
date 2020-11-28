using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Models;

namespace FoodDelivery.Pages.Admin.User {
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public IndexModel(ApplicationDbContext context, UserManager<IdentityUser> userManager) {
            _context = context;
            _userManager = userManager;
        }
        public IList<ApplicationUser> ApplicationUsers { get; set; }
        public Dictionary<string, List<string>> UserRoles { get; set; }
        public async Task OnGetAsync() {
            UserRoles = new Dictionary<string, List<string>>();
            ApplicationUsers = _context.ApplicationUser.AsEnumerable().ToList();

            foreach (var user in ApplicationUsers) {
                var userRole = await _userManager.GetRolesAsync(user);
                UserRoles.Add(user.Id, userRole.ToList());
            }
        }
        public async Task<IActionResult> OnPostLockUnlock(string id) {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user.LockoutEnd == null) {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            } else if (user.LockoutEnd > DateTime.Now) {
                user.LockoutEnd = DateTime.Now;
            } else {
                user.LockoutEnd = DateTime.Now.AddYears(100);
            }
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return RedirectToPage();
        }
    }
}
