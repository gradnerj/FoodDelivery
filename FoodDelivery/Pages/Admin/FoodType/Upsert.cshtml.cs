using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FoodDelivery.Pages.Admin.FoodType {
    public class UpsertModel : PageModel
    {

        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        [BindProperty]
        public Models.FoodType FoodTypeObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
           // _unitOfWork = unitOfWork;
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            FoodTypeObj = new Models.FoodType();
            if (id != null)
            {
                //FoodTypeObj = _unitOfWork.FoodType.GetFirstorDefault(u => u.Id == id);
                FoodTypeObj = _context.FoodType.Where(u => u.Id == id).FirstOrDefault();
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
                //_unitOfWork.FoodType.Add(FoodTypeObj);
                _context.FoodType.Add(FoodTypeObj);
            }
            else
            {
                // Update Category object
                //_unitOfWork.FoodType.Update(FoodTypeObj);
                _context.FoodType.Update(FoodTypeObj);
            }
            // _unitOfWork.Save();
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
