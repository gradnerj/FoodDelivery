using System.Linq;
using System.Security.Claims;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace FoodDelivery.Pages.Customer.Home {
    public class DetailsModel : PageModel
    {
       // private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public DetailsModel(IUnitOfWork unitOfWork, ApplicationDbContext context) {
            //_unitOfWork = unitOfWork;
            _context = context;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            //ShoppingCartObj = new ShoppingCart() {
            //    MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(includeProperties: "Category,FoodType",
            //    filter: c => c.Id == id),
            //    MenuItemId = id
            //};
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
                //  ShoppingCart cartFromDb = _unitOfWork.ShoppingCart.GetFirstorDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);
                 ShoppingCart cartFromDb = _context.ShoppingCart.FirstOrDefault(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId && c.MenuItemId == ShoppingCartObj.MenuItemId);

                if (cartFromDb == null) {
                    //_unitOfWork.ShoppingCart.Add(ShoppingCartObj);
                    _context.ShoppingCart.Add(ShoppingCartObj);
                } else {
                    //_unitOfWork.ShoppingCart.IncrementCount(cartFromDb, ShoppingCartObj.Count);
                    cartFromDb.Count += ShoppingCartObj.Count;
                    _context.ShoppingCart.Update(cartFromDb);
                }
                //_unitOfWork.Save();
                _context.SaveChanges();
                //var count = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                var count = _context.ShoppingCart.Where(c => c.ApplicationUserId == ShoppingCartObj.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ShoppingCart,count);
                return RedirectToPage("Index");
            } else {
                //ShoppingCartObj.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(includeProperties: "Category,FoodType",
                //filter: c => c.Id == ShoppingCartObj.MenuItemId);
                ShoppingCartObj.MenuItem = _context.MenuItem.Where(m => m.Id == ShoppingCartObj.MenuItemId)
                    .Include(m => m.Category)
                    .Include(m => m.FoodType)
                    .FirstOrDefault();
            }
                return Page();
            
        }
    }
}
