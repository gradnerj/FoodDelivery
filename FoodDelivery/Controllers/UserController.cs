using System;
using System.Linq;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]

    public class UserController : Controller
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            //_unitOfWork = unitOfWork;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {

            //return Json(new { data = _unitOfWork.ApplicationUser.GetAll() });
            return Json(new { data = _context.ApplicationUser.AsEnumerable() });
        }

        [HttpPost]
        public IActionResult LockUnlock([FromBody]string id)
        {
            // var objFromDb = _unitOfWork.ApplicationUser.GetFirstorDefault(u => u.Id == id);
            var objFromDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return Json(new { success = false, message = "Error while Locking / Unlocking" });
            }

            if(objFromDb.LockoutEnd!=null && objFromDb.LockoutEnd > DateTime.Now)
            {
                //User is currently locked out so unlock them
                objFromDb.LockoutEnd = DateTime.Now;
            }

            else
            {
                //lock user
                objFromDb.LockoutEnd = DateTime.Now.AddYears(100);
            }


            //_unitOfWork.Save();
            _context.SaveChanges();
            return Json(new { success = true, message = "Lock/Unlock Successful" });

        }
    }
}