﻿using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    // Maybe inherit from Controller vs. ControllerBase
    public class CategoryController : Controller {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.Category.List() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.Category.Get(c => c.Id == id);
            if(objFromDb == null) {
                return Json(new { success = false, message = "Error while deleting"});
            }
            _unitOfWork.Category.Delete(objFromDb);
            _unitOfWork.Commit();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
