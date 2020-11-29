using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces {
    public interface IUnitOfWork {
        IGenericRepository<ApplicationUser> ApplicationUser { get;  }
        IGenericRepository<Category> Category { get; }
        IGenericRepository<FoodType> FoodType { get; }
        IGenericRepository<MenuItem> MenuItem { get; }
        IGenericRepository<OrderDetails> OrderDetails { get; }
        IGenericRepository<OrderHeader> OrderHeader { get; }
        IGenericRepository<ShoppingCart> ShoppingCart { get; }
        Task<int> CommitAsync();
        int Commit();
    }
}
