using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class UnitOfWork : IUnitOfWork {

        private readonly ApplicationDbContext _context;
        public ICategoryRepository Category { get; private set; }
        public IFoodTypeRepository FoodType { get; private set; }




        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
            Category = new CategoryRepository(_context);
            FoodType = new FoodTypeRepository(_context);
        }

        public void Dispose() {
            _context.Dispose();
        }

        public void Save() {
            _context.SaveChanges();
        }
    }
}
