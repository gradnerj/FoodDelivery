using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FoodDelivery.Pages.Admin.MenuItem {

    public class UpsertModel : PageModel {

        private ApplicationDbContext _context;
        private readonly Microsoft.AspNetCore.Hosting.IWebHostEnvironment _hostEnvironment;


        [BindProperty]
        public MenuItemVM MenuItemObj { get; set; }

        public UpsertModel(Microsoft.AspNetCore.Hosting.IWebHostEnvironment hostEnvironment, ApplicationDbContext context) {
            _hostEnvironment = hostEnvironment;
            _context = context;
        }

        public IActionResult OnGet(int? id) {
            MenuItemObj = new MenuItemVM {
                MenuItem = new Models.MenuItem(),
                CategoryList = _context.Category.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }),
                FoodTypeList = _context.FoodType.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.Name }),
            };

            if (id != 0) {
                MenuItemObj.MenuItem = _context.MenuItem.AsNoTracking().Where(u => u.Id == id).FirstOrDefault();
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
                _context.MenuItem.Add(MenuItemObj.MenuItem);
            } else {
                // Update MenuItem object
                var objFromDb = _context.MenuItem.AsNoTracking().Where(m => m.Id == MenuItemObj.MenuItem.Id).FirstOrDefault();
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

                }
                _context.MenuItem.Update(MenuItemObj.MenuItem);
            }
            _context.SaveChanges();
            return RedirectToPage("./Index");
        }
    }
}
