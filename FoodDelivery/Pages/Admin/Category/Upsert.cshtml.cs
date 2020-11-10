using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FoodDelivery.Pages.Admin.Category {

    public class UpsertModel : PageModel {

        //private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Models.Category CategoryObj { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, ApplicationDbContext context) {
           // _unitOfWork = unitOfWork;
            _context = context;
        }

        // IActionResult lets you return many many things
        public IActionResult OnGet(int? id) {
            CategoryObj = new Models.Category();

            if (id != 0) {
                //CategoryObj = _unitOfWork.Category.GetFirstorDefault(u => u.Id == id);
                CategoryObj = _context.Category.Where(u => u.Id == id).FirstOrDefault();
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
                //_unitOfWork.Category.Add(CategoryObj);
                _context.Category.Add(CategoryObj);
            }
            else {
                // Update Category object
                // _unitOfWork.Category.Update(CategoryObj);
                _context.Category.Update(CategoryObj);
            }
            // _unitOfWork.Save();
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
