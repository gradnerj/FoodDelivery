using System.Linq;
using System.Security.Claims;
using Infrastructure.Data;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery.Pages.Customer.Home {
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        public DetailsModel(ApplicationDbContext context) {
            _context = context;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart() {
                MenuItem = _context.MenuItem.Where(m => m.Id == id)
                .Include(x => x.Category)
                .Include(y => y.FoodType)
                .FirstOrDefault()
            };
            ShoppingCartObj.MenuItemId = id;
        }
        public IActionResult OnPost() {
            if (ModelState.IsValid) {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCartObj.ApplicationUserId = claim.Value;
                 ShoppingCart cartFromDb = _context.ShoppingCart.FirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);

                if (cartFromDb == null) {
                    _context.ShoppingCart.Add(ShoppingCartObj);
                } else {
                    cartFromDb.Count += ShoppingCartObj.Count;
                    _context.ShoppingCart.Update(cartFromDb);
                }
                _context.SaveChanges();
                var count = _context.ShoppingCart.Where(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ShoppingCart,count);
                return RedirectToPage("Index");
            } else {
                ShoppingCartObj.MenuItem = _context.MenuItem.Where(m => m.Id == ShoppingCartObj.MenuItemId)
                    .Include(m => m.Category)
                    .Include(m => m.FoodType)
                    .FirstOrDefault();
            }
                return Page();
            
        }
    }
}
