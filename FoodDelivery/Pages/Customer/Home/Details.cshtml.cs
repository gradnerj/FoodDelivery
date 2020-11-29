using System.Linq;
using System.Security.Claims;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;

namespace FoodDelivery.Pages.Customer.Home {
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart() {
                MenuItem = _unitOfWork.MenuItem.Get(m => m.Id == id, false, "Category,FoodType")
            };
            ShoppingCartObj.MenuItemId = id;
        }
        public IActionResult OnPost() {
            if (ModelState.IsValid) {
                var claimsIdentity = (ClaimsIdentity)this.User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ShoppingCartObj.ApplicationUserId = claim.Value;
                ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.Get(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);
                if (cartFromDb == null) {
                    _unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                } else {
                    cartFromDb.Count += ShoppingCartObj.Count;
                    _unitOfWork.ShoppingCart.Update(cartFromDb);
                }
                _unitOfWork.Commit();
                var count = _unitOfWork.ShoppingCart.List(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).Count();
                HttpContext.Session.SetInt32(SD.ShoppingCart,count);
                return RedirectToPage("Index");
            } else {
                ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.Get(m => m.Id == ShoppingCartObj.MenuItemId, false, "Category,FoodType");
            }
                return Page();
        }
    }
}
