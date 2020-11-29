using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data {
    public class UnitOfWork : IUnitOfWork {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context) {
            _context = context;
        }
        private IGenericRepository<ApplicationUser> _ApplicationUser;
        private IGenericRepository<Category> _Category;
        private IGenericRepository<FoodType> _FoodType;
        private IGenericRepository<MenuItem> _MenuItem;
        private IGenericRepository<OrderDetails> _OrderDetails;
        private IGenericRepository<OrderHeader> _OrderHeader;
        private IGenericRepository<ShoppingCart> _ShoppingCart;
        public IGenericRepository<ApplicationUser> ApplicationUser {
            get {
                if (_ApplicationUser == null) _ApplicationUser = new GenericRepository<ApplicationUser>(_context);
                return _ApplicationUser;
            }
        }

        public IGenericRepository<Category> Category {
            get {
                if (_Category == null) _Category = new GenericRepository<Category>(_context);
                return _Category;
            }
        }

        public IGenericRepository<FoodType> FoodType {
            get {
                if (_FoodType == null) _FoodType = new GenericRepository<FoodType>(_context);
                return _FoodType;
            }
        }

        public IGenericRepository<MenuItem> MenuItem {
            get {
                if (_MenuItem == null) _MenuItem = new GenericRepository<MenuItem>(_context);
                return _MenuItem;
            }
        }

        public IGenericRepository<OrderDetails> OrderDetails {
            get {
                if (_OrderDetails == null) _OrderDetails = new GenericRepository<OrderDetails>(_context);
                return _OrderDetails;
            }
        }

        public IGenericRepository<OrderHeader> OrderHeader {
            get {
                if (_OrderHeader == null) _OrderHeader = new GenericRepository<OrderHeader>(_context);
                return _OrderHeader;
            }
        }

        public IGenericRepository<ShoppingCart> ShoppingCart { 
            get {
                if (_ShoppingCart == null) _ShoppingCart = new GenericRepository<ShoppingCart>(_context);
                return _ShoppingCart;
            }
        }

        public int Commit() {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync() {
            return await _context.SaveChangesAsync();
        }

        public void Dispose() => _context.Dispose();
    }
}
