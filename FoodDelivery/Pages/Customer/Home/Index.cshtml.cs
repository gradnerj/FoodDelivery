using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Pages.Customer.Home {
    public class IndexModel : PageModel {
        private readonly ApplicationDbContext _context;
        public IndexModel(ApplicationDbContext context) {
            _context = context;
        }

        public IEnumerable<MenuItem> MenuItemList { get; set; } 
        public IEnumerable<Category> CategoryList { get; set; } 
        public void OnGet() {
            MenuItemList = _context.MenuItem.Include(c => c.Category).Include(f => f.FoodType).AsEnumerable();
            CategoryList = _context.Category.OrderBy(q => q.DisplayOrder).ToList().AsEnumerable();
        } 
    } 
}
