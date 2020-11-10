using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using FoodDelivery.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Pages.Customer.Home {
    public class IndexModel : PageModel { 
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public IndexModel(IUnitOfWork unitOfWork, ApplicationDbContext context) { 
            //_unitOfWork = unitOfWork;
            _context = context;
        } 
        public IEnumerable<MenuItem> MenuItemList { get; set; } 
        public IEnumerable<Category> CategoryList { get; set; } 
        public void OnGet() {
            // MenuItemList = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType"); 
            MenuItemList = _context.MenuItem.Include(c => c.Category).Include(f => f.FoodType).AsEnumerable();
            //CategoryList = _unitOfWork.Category.GetAll(null, q => q.OrderBy(c => c.DisplayOrder), null); 
            CategoryList = _context.Category.OrderBy(q => q.DisplayOrder).ToList().AsEnumerable();
        } 
    } 
}
