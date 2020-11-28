using System;
using System.Linq;
using DataAccess.Data;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult Get()
        {

            return Json(new { data = _context.ApplicationUser.AsEnumerable() });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            var objFromDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking / Unlocking" });
            }

            if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now)
            {
                objFromDb.LockoutEnd = DateTime.Now;
            }
            else
            {
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }

            _context.SaveChanges();
            return Json(new { success = true, message = "Lock/Unlock Successful" });

        }
    }
}