using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FoodDelivery.Pages.Admin.FoodType
{
    public class UpsertModel : PageModel
    {

        private readonly IUnitOfWork _unitOfWork;

        [BindProperty]
        public FoodDelivery.Models.FoodType FoodTypeObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Models.FoodType();
            if (id != null)
            {
                FoodTypeObj = _unitOfWork.FoodType.GetFirstorDefault(u => u.Id == id);
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
                _unitOfWork.FoodType.Add(FoodTypeObj);
                
            }
            else
            {
                // Update Category object
                _unitOfWork.FoodType.Update(FoodTypeObj);
            }
            _unitOfWork.Save();
            return RedirectToPage("./Index");
        }
    }
}
