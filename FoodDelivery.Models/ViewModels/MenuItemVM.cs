using FoodDelivery.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace FoodDelivery.Models.ViewModels {
    public class MenuItemVM {
        public MenuItem MenuItem { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> FoodTypeList { get; set; }
    }
}
