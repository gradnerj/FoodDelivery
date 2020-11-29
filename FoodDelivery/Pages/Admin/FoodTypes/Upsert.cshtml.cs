using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace FoodDelivery.Pages.Admin.FoodTypes {
    public class UpsertModel : PageModel
    {
        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public FoodType FoodTypeObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new FoodType();
            if (id != null)
            {
                FoodTypeObj = _unitOfWork.FoodType.Get(f => f.Id == id);
                if (FoodTypeObj == null)
                {
                    return NotFound();
                }
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if (FoodTypeObj.Id == 0)
            {
                var foodTypes = _unitOfWork.FoodType.List();
                if (!foodTypes.Any(f => f.Name == FoodTypeObj.Name)) {
                    _unitOfWork.FoodType.Add(FoodTypeObj);
                }
            }
            else
            {
                _unitOfWork.FoodType.Update(FoodTypeObj);
            }
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
