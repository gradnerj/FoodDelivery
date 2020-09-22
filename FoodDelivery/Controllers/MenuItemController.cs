using System;
using System.Collections.Generic;
using System.IO;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostingEnv;
        public MenuItemController(IUnitOfWork unitOfWork, IWebHostEnvironment hostingEnv)
        {
            _unitOfWork = unitOfWork;
            _hostingEnv = hostingEnv;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.MenuItem.GetAll(null, null, "Category,FoodType") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                var objFromDb = _unitOfWork.MenuItem.GetFirstorDefault(u => u.Id == id);
                if (objFromDb == null) {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                var imgPath = Path.Combine(_hostingEnv.WebRootPath, objFromDb.Image.TrimStart('\\'));
                if (System.IO.File.Exists(imgPath)) {
                    System.IO.File.Delete(imgPath);
                }
                _unitOfWork.MenuItem.Remove(objFromDb);
                _unitOfWork.Save();
                
            }
            catch(Exception e) {
                return Json(new { success = false, message = "Error while deleting" });
            }
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
