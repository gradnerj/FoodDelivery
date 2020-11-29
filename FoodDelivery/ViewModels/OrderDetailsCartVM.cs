using ApplicationCore.Models;
using System.Collections.Generic;

namespace FoodDelivery.ViewModels {
    public class OrderDetailsCartVM {
        public OrderHeader OrderHeader { get; set; }
        public List<ShoppingCart> ListCart { get; set; }
    }
}
