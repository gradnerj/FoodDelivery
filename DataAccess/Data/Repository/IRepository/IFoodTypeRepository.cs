using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;


namespace FoodDelivery.DataAccess.Data.Repository.IRepository {
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeListForDropDown();
        void Update(FoodType foodType);
    }
}
