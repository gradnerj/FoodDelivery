using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models.Models;

namespace FoodDelivery.DataAccess.Data.Repository.IRepository {
    public interface IMenuItemRepository : IRepository<MenuItem> {

        void Update(MenuItem menuItem);

    }
}
