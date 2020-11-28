using FoodDelivery.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace FoodDelivery.Pages.Admin.Category {

    public class UpsertModel : PageModel {

        
        private readonly ApplicationDbContext _context;
        [BindProperty]
        public Models.Category CategoryObj { get; set; }
        public UpsertModel(ApplicationDbContext context) {
            _context = context;
        }

        // IActionResult lets you return many many things
        public IActionResult OnGet(int? id) {
            CategoryObj = new Models.Category();

            if (id != 0) {
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
                _context.Category.Add(CategoryObj);
            }
            else {
                _context.Category.Update(CategoryObj);
            }
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
