using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Models.ViewModels;
using System.Security.Claims;
using System.Linq;

namespace FoodDelivery.Pages.Customer.Cart {
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        public IndexModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OrderDetailsCartVM OrderDetailsCart { get; set; }

        public void OnGet()
        {
            OrderDetailsCart = new OrderDetailsCartVM() {
                OrderHeader = new OrderHeader(),
                ListCart = new List<ShoppingCart>()
            };
            OrderDetailsCart.OrderHeader.OrderTotal = 0;
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null) {
                IEnumerable<ShoppingCart> cart = _unitOfWork.ShoppingCart.GetAll(c => c.ApplicationUserId == claim.Value);
                if (cart != null) {
                    OrderDetailsCart.ListCart = cart.ToList();
                }
                foreach(var cartList in OrderDetailsCart.ListCart) {
                    cartList.MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(n => n.Id == cartList.MenuItemId);
                    OrderDetailsCart.OrderHeader.OrderTotal += (cartList.MenuItem.Price * cartList.Count);
                }

            }

        }

       
    }
}