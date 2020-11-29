using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using ApplicationCore.Interfaces;
using System.Threading.Tasks;

namespace FoodDelivery.Pages.Customer.Home {
    public class IndexModel : PageModel {
        private readonly IUnitOfWork _unitOfWork;
        public IndexModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IEnumerable<MenuItem> MenuItemList { get; set; } 
        public IEnumerable<Category> CategoryList { get; set; } 
        public void OnGet() {
            MenuItemList =  _unitOfWork.MenuItem.List(null, null, "Category,FoodType");
            CategoryList =  _unitOfWork.Category.List(null, q => q.DisplayOrder, null);
        } 
    } 
}
