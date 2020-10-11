using System;

namespace FoodDelivery.DataAccess.Data.Repository.IRepository {
    public interface IUnitOfWork : IDisposable {
        ICategoryRepository Category { get; }
        IFoodTypeRepository FoodType { get; }

        IMenuItemRepository MenuItem { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IShoppingCartRepository ShoppingCart { get; }
        void Save();

    }
}
