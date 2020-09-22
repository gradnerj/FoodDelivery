using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace FoodDelivery.DataAccess.Data.Repository {
    public class FoodTypeRepository : Repository<Models.FoodType>, IFoodTypeRepository
    {

        private readonly ApplicationDbContext _context;

        public FoodTypeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;

        }
        public IEnumerable<SelectListItem> GetFoodTypeListForDropDown()
        {
            return _context.FoodType.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.Id.ToString()
            });
        }

        public void Update(FoodDelivery.Models.FoodType foodType)
        {
            var objFromDb = _context.FoodType.FirstOrDefault(f => f.Id == foodType.Id);
            objFromDb.Name = foodType.Name;
            _context.SaveChanges();
        }
    }
}

