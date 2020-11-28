using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    // Maybe inherit from Controller vs. ControllerBase
    public class CategoryController : Controller {

        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context) {
            _context = context;

        }

        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _context.Category.AsEnumerable() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var objFromDb = _context.Category.FirstOrDefault(c => c.Id == id);
            if(objFromDb == null) {
                return Json(new { success = false, message = "Error while deleting"});
            }
            _context.Category.Remove(objFromDb);
            _context.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
