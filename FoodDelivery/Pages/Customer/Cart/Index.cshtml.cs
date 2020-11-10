using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Models.ViewModels;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using FoodDelivery.Data;

namespace FoodDelivery.Pages.Customer.Cart {
    public class IndexModel : PageModel {
       // private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;
        public IndexModel(IUnitOfWork unitOfWork, ApplicationDbContext context) {
           // _unitOfWork = unitOfWork;
            _context = context;
        }

        public OrderDetailsCartVM OrderDetailsCart { get; set; }

        public void OnGet() {
            OrderDetailsCart = new OrderDetailsCartVM() {
                OrderHeader = new OrderHeader(),
                ListCart = new List<ShoppingCart>()
            };
            OrderDetailsCart.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) {
                //IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);
                IEnumerable<ShoppingCart> cart = _context.ShoppingCart.Where(c => c.ApplicationUserId == claim.Value);
                if (cart != null) {
                    OrderDetailsCart.ListCart = cart.ToList();
                }
                foreach (var cartList in OrderDetailsCart.ListCart) {
                    //cartList.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(n => n.Id == cartList.MenuItemId);
                    cartList.MenuItem = _context.MenuItem.FirstOrDefault(n => n.Id == cartList.MenuItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
                }

            }

        }

        public IActionResult OnPostMinus(int cartId) {
            // var cart = _unitOfWork.ShoppingCart.GetFirstorDefault(c => c.Id == cartId);
            var cart = _context.ShoppingCart.FirstOrDefault(c => c.Id == cartId);
            if (cart.Count == 1) {
                //_unitOfWork.ShoppingCart.Remove(cart);
                _context.ShoppingCart.Remove(cart);

            } else {
                //_unitOfWork.ShoppingCart.DecrementCount(cart, 1);
                cart.Count -= 1;
                _context.ShoppingCart.Update(cart);
            }
            // _unitOfWork.Save();
            _context.SaveChanges();

            //var cnt = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            var cnt = _context.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostPlus(int cartId) {
            //var cart = _unitOfWork.ShoppingCart.GetFirstorDefault(c => c.Id == cartId);
            var cart = _context.ShoppingCart.FirstOrDefault(c => c.Id == cartId);
            cart.Count += 1;
            _context.ShoppingCart.Update(cart);
            //_unitOfWork.ShoppingCart.IncrementCount(cart, 1);
            //_unitOfWork.Save();
            _context.SaveChanges();
            return RedirectToPage("/Customer/Cart/Index");
        }
        public IActionResult OnPostRemove(int cartId) {
            //var cart = _unitOfWork.ShoppingCart.GetFirstorDefault(c => c.Id == cartId);
            var cart = _context.ShoppingCart.FirstOrDefault(c => c.Id == cartId);
            //_unitOfWork.ShoppingCart.Remove(cartId);
            _context.ShoppingCart.Remove(cart);
            //_unitOfWork.Save();
            _context.SaveChanges();


            //var cnt = _unitOfWork.ShoppingCart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            var cnt = _context.ShoppingCart.Where(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.ShoppingCart, cnt);
            return RedirectToPage("/Customer/Cart/Index");
        }
    }
}