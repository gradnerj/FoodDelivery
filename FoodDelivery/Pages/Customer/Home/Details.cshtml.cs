using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Customer.Home
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;
        public DetailsModel(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        [BindProperty]
        public ShoppingCart ShoppingCartObj { get; set; }
        public void OnGet(int id)
        {
            ShoppingCartObj = new ShoppingCart() {
                MenuItem = _unitOfWork.MenuItem.GetFirstorDefault(includeProperties: "Category,FoodType",
                filter: c => c.Id == id),
                MenuItemId = id
            };
        }
    }
}
