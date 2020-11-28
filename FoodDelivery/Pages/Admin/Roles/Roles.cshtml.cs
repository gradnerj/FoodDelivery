using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.Roles
{
    public class RolesModel : PageModel {
        private ApplicationDbContext _context;
        private RoleManager<IdentityRole> _roleManager;
        public RolesModel(ApplicationDbContext context, RoleManager<IdentityRole> roleManager) {
            _context = context;
            _roleManager = roleManager;
        }
        public List<string> Roles { get; set; }
        [BindProperty]
        public string RoleName { get; set; }
        [BindProperty]
        public string NewRole { get; set; }

        public void OnGet() {
            Roles = _roleManager.Roles.Select(r => r.Name).ToList();


        }
        public async Task<IActionResult> OnPostAsync() {
            var newRole = new IdentityRole();
            newRole.Name = RoleName;
            await _roleManager.CreateAsync(newRole);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostUpdateAsync() {
            var roleToUpdate = await _roleManager.FindByNameAsync(RoleName);
            roleToUpdate.Name = NewRole;
            await _roleManager.UpdateAsync(roleToUpdate);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync() {
            var roleToDelete = await _roleManager.FindByNameAsync(RoleName);
            await _roleManager.DeleteAsync(roleToDelete);
            return RedirectToPage();
        }
    }
}
