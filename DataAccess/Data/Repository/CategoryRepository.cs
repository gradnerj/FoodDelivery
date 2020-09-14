using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class CategoryRepository : Repository<Category>, ICategoryRepository {

        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetCategoryListForDropDown() {
            return _context.Category.Select(i => new SelectListItem() {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(Category category) {
            var objFromDb = _context.Category.FirstOrDefault(c => c.Id == category.Id);
            objFromDb.Name = category.Name;
            objFromDb.DisplayOrder = category.DisplayOrder;
            _context.SaveChanges();
        }
    }
}
