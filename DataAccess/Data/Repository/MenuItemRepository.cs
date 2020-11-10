using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using System.Linq;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class MenuItemRepository : Repository<MenuItem>, IMenuItemRepository {
        private readonly ApplicationDbContext _context;
        public MenuItemRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }


        public void Update(MenuItem menuItem) {
            var menuItemObj =  _context.MenuItem.FirstOrDefault(m => m.Id == menuItem.Id);
            menuItemObj.Name = menuItem.Name;
            menuItemObj.CategoryId = menuItem.CategoryId;
            menuItemObj.Description = menuItem.Description;
            menuItemObj.FoodTypeId = menuItem.FoodTypeId;
            menuItemObj.Price = menuItem.Price;

            if(menuItem.Image != null) {
                menuItemObj.Image = menuItem.Image;
            }
            _context.SaveChanges();
        }

    }
}
