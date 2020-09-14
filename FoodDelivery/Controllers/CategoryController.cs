﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace FoodDelivery.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    // Maybe inherit from Controller vs. ControllerBase
    public class CategoryController : Controller {

        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;

        }

        [HttpGet]
        public IActionResult Get() {
            return Json(new { data = _unitOfWork.Category.GetAll() });
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            var objFromDb = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
            if(objFromDb == null) {
                return Json(new { success = false, message = "Error while deleting"});
            }
            _unitOfWork.Category.Remove(objFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
    }
}
