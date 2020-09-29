using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Models;

namespace FoodDelivery.Pages.Customer.Home {
    public class IndexModel : PageModel { 
        private readonly IUnitOfWork _unitOfWork; 
        public IndexModel(IUnitOfWork unitOfWork) { 
            _unitOfWork = unitOfWork; 
        } 
        public IEnumerable<MenuItem> MenuItemList { get; set; } 
        public IEnumerable<Category> CategoryList { get; set; } 
        public void OnGet() { 
            MenuItemList = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType"); 
            CategoryList = _unitOfWork.Category.GetAll(null, q => q.OrderBy(c => c.DisplayOrder), null); 
        } 
    } 
}
