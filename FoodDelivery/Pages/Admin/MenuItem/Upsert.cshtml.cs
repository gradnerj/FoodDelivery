using FoodDelivery.DataAccess.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace FoodDelivery.Pages.Admin.MenuItem {

    public class UpsertModel : PageModel {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHostEnvironment _hostEnvironment;


        [BindProperty]
        public Models.Models.MenuItem MenuItemObj { get; set; }
        public UpsertModel(IUnitOfWork unitOfWork, IHostEnvironment hostEnvironment) {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        // IActionResult lets you return many many things
        public IActionResult OnGet(int? id) {
            MenuItemObj = new Models.Models.MenuItem();

            if (id != 0) {
                MenuItemObj = _unitOfWork.MenuItem.GetFirstorDefault(u => u.Id == id);
                if (MenuItemObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost() {
            if (!ModelState.IsValid) {
                return Page();
            }
            if (MenuItemObj.Id == 0) {
                _unitOfWork.MenuItem.Add(MenuItemObj);
            } else {
                // Update MenuItem object
                _unitOfWork.MenuItem.Update(MenuItemObj);
            }
            _unitOfWork.Save();

            return RedirectToPage("./Index");
        }
    }
}
