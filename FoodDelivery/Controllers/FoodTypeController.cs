using System.Linq;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public FoodTypeController(IUnitOfWork unitOfWork, ApplicationDbContext context) 
        {
            //_unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Json(new { data = _unitOfWork.FoodType.GetAll() });
            return Json(new { data = _context.FoodType.AsEnumerable() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            // var objFromDb = _unitOfWork.FoodType.GetFirstorDefault(u => u.Id == id);
            var objFromDb = _context.FoodType.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            //_unitOfWork.FoodType.Remove(objFromDb);
            _context.FoodType.Remove(objFromDb);
            //_unitOfWork.Save();
            _context.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
