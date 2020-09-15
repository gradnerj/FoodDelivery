using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SQLitePCL;

namespace FoodDelivery.Pages.Admin.Category {

    public class UpsertModel : PageModel {

        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public FoodDelivery.Models.Category CategoryObj { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }

        // IActionResult lets you return many many things
        public IActionResult OnGet(int? id) {
            CategoryObj = new Models.Category();
            if (id != null) {
                CategoryObj = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
                if (CategoryObj == null) {
                    return NotFound();
                }
            }
            return Page();
        }

        public void OnPost() {

        }
    }
}