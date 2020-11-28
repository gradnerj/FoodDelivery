using System.Linq;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class FoodTypeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public FoodTypeController(ApplicationDbContext context) 
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _context.FoodType.AsEnumerable() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var objFromDb = _context.FoodType.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            _context.FoodType.Remove(objFromDb);
            _context.SaveChanges();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
