using System;
using System.IO;
using System.Linq;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
       // private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly ApplicationDbContext _context;
        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnv, ApplicationDbContext context)
        {
            //_unitOfWork = unitOfWork;
            _hostingEnv = hostingEnv;
            _context = context;
        }

        [HttpGet]
        public IActionResult Get()
        {
            //return Json(new { data = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType") });
            return Json(new { data = _context.MenuItem.Include(m => m.Category).Include(c => c.FoodType).AsEnumerable() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                // var objFromDb = _unitOfWork.MenuItem.GetFirstorDefault(u => u.Id == id);
                var objFromDb = _context.MenuItem.FirstOrDefault(u => u.Id == id);
                if (objFromDb == null) {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                // Had to add this additional control block to check if menu item has an image path in the database
                if (objFromDb.Image != null) {
                    var imgPath = Path.Combine(_hostingEnv.WebRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imgPath)) {
                        System.IO.File.Delete(imgPath);
                    }
                }
                // _unitOfWork.MenuItem.Remove(objFromDb);
                _context.MenuItem.Remove(objFromDb);
                // _unitOfWork.Save();
                _context.SaveChanges();
                
            }
            catch(Exception e) {
                throw e;
                //return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
