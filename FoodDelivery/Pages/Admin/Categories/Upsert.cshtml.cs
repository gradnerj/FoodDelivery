using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace FoodDelivery.Pages.Admin.Categories {

    public class UpsertModel : PageModel {

        private readonly IUnitOfWork _unitOfWork;
        
        [BindProperty]
        public Category CategoryObj { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        // IActionResult lets you return many many things
        public IActionResult OnGet(int? id) {
            CategoryObj = new Category();

            if (id != 0) {
                CategoryObj = _unitOfWork.Category.Get(u => u.Id == id);
                if (CategoryObj == null) {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }
            if(CategoryObj.Id == 0) {
                _unitOfWork.Category.Add(CategoryObj);
            }
            else {
                _unitOfWork.Category.Update(CategoryObj);
            }
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
