using System;
using System.IO;
using System.Linq;
using ApplicationCore.Interfaces;
using Infrastructure.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnv;
        private readonly IUnitOfWork _unitOfWork;
        public MenuItemController(IWebHostEnvironment hostingEnv, IUnitOfWork unitOfWork)
        {
            _hostingEnv = hostingEnv;
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Json(new { data = _unitOfWork.MenuItem.List(null, null, "Category,FoodType") });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try {
                var objFromDb = _unitOfWork.MenuItem.Get(m => m.Id == id);
                if (objFromDb == null) {
                    return Json(new { success = false, message = "Error while deleting" });
                }
                if (objFromDb.Image != null) {
                    var imgPath = Path.Combine(_hostingEnv.WebRootPath, objFromDb.Image.TrimStart('\\'));
                    if (System.IO.File.Exists(imgPath)) {
                        System.IO.File.Delete(imgPath);
                    }
                }
                _unitOfWork.MenuItem.Delete(objFromDb);
                _unitOfWork.Commit();
            }
            catch(Exception e) {
                throw e;
            }
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
