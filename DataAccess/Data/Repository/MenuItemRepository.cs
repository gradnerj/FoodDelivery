using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository {
        private readonly ApplicationDbContext _context;
        public MenuItemRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }


        public void Update(MenuItem menuItem) {
            var menuItemObj = _context.MenuItem.FirstOrDefault(m => m.Id == menuItem.Id);
        }

    }
}
