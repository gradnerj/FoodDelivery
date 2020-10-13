using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.DataAccess.Data.Repository.IRepository {
    public interface IOrderDetailsRepository : IRepository<OrderDetails>{
           void Update(OrderDetails orderDetails);
    }
}
