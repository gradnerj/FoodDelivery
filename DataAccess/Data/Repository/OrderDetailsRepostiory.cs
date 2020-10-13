using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class OrderDetailsRepostiory  : Repository<OrderDetails>, IOrderDetailsRepository{
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepostiory(ApplicationDbContext db):base(db) {
            _db = db;
        }


        public void Update(OrderDetails orderDetails) {
            throw new NotImplementedException();
        }

    }
}
