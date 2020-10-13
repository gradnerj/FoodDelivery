using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository {
        private readonly ApplicationDbContext _db;
        public OrderHeaderRepository(ApplicationDbContext db):base(db) {
            _db = db;

        }

        public void Update(OrderHeader orderHeader) {
            throw new NotImplementedException();
        }
    }
}
