using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    // Maybe inherit from Controller vs. ControllerBase
    public class CategoryController : Controller {

        // private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public CategoryController(IUnitOfWork unitOfWork, ApplicationDbContext context) {
            //_unitOfWork = unitOfWork;
            _context = context;

        }

        [HttpGet]
        public IActionResult Get() {
            // return Json(new { data = _unitOfWork.Category.GetAll() });
            return Json(new { data = _context.Category.AsEnumerable() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            //var objFromDb = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
            var objFromDb = _context.Category.FirstOrDefault(c => c.Id == id);
            if(objFromDb == null) {
                return Json(new { success = false, message = "Error while deleting"});
            }
            // _unitOfWork.Category.Remove(objFromDb);
            _context.Category.Remove(objFromDb);
            //_unitOfWork.Save();
            _context.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
