using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

    }
}
