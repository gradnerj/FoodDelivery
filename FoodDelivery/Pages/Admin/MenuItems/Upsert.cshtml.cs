using Infrastructure.Data;
using FoodDelivery.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;
using ImageResizer;
namespace FoodDelivery.Pages.Admin.MenuItems {

    public class UpsertModel : PageModel {
        private readonly IUnitOfWork _unitOfWork;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostEnvironment;



        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }

        public UpsertModel(IUnitOfWork unitOfWork, Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment) {
            _hostEnvironment = hostEnvironment;
            _unitOfWork = unitOfWork;
        }

        public IActionResult OnGet(int? id) {
            var categories = _unitOfWork.Category.List();
            var foodTypes = _unitOfWork.FoodType.List();
            MenuItemObj = new MenuItemVM {
                MenuItem = new MenuItem(),
                CategoryList = categories.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                FoodTypeList = foodTypes.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }),
            };

            if (id != 0) {
                MenuItemObj.MenuItem = _unitOfWork.MenuItem.Get(u => u.Id == id, true);
                if (MenuItemObj == null) {
                    return NotFound();
                }
            }

            return Page();
        }

        public IActionResult OnPost() {

            string webRootPath = _hostEnvironment.WebRootPath;
            var files = HttpContext.Request.Form.Files;

            if (!ModelState.IsValid) {
                return Page();
            }
            if (MenuItemObj.MenuItem.Id == 0) {
                if (files.Count > 0) {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems\");
                    var extension = Path.GetExtension(files[0].FileName);
                    var fullpath = uploads + fileName + extension;
                    using (var fileStream = System.IO.File.Create(fullpath)) {
                        files[0].CopyTo(fileStream);
                    }
                    MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension;
                }
                _unitOfWork.MenuItem.Add(MenuItemObj.MenuItem);
            } else {
                // Update MenuItem object
                var objFromDb = _unitOfWork.MenuItem.Get(m => m.Id == MenuItemObj.MenuItem.Id, true);
                if(files.Count > 0) {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(webRootPath, @"images\menuitems\");
                    var extension = Path.GetExtension(files[0].FileName);
                    if (objFromDb.Image != null) {
                        var imagePath = Path.Combine(webRootPath, objFromDb.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(imagePath)) {
                            System.IO.File.Delete(imagePath);
                        }
                    }
                    var fullpath = uploads + fileName + extension;
                    using (var fileStream = System.IO.File.Create(fullpath)) {

                        files[0].CopyTo(fileStream);
                        
                    }
                    MenuItemObj.MenuItem.Image = @"\images\menuitems\" + fileName + extension;
                    
                } else {
                    MenuItemObj.MenuItem.Image = objFromDb.Image;
                }

               
                _unitOfWork.MenuItem.Update(MenuItemObj.MenuItem);
            }
            _unitOfWork.Commit();
            return RedirectToPage("./Index");
        }
    }
}
