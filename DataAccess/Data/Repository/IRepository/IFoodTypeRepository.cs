using FoodDelivery.DataAccess.IRepository;
using FoodDelivery.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace FoodDelivery.DataAccess.Data.Repository.IRepository {
    public interface IFoodTypeRepository : IRepository<FoodType>
    {
        IEnumerable<SelectListItem> GetFoodTypeListForDropDown();
        void Update(FoodType foodType);
    }
}
