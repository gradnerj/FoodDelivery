using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using FoodDelivery.Data;

namespace FoodDelivery.Pages.Admin {
    public class CategoryIndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CategoryIndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<FoodDelivery.Models.Category> Category { get;set; }

        public async Task OnGetAsync()
        {
            Category = await _context.Category.ToListAsync();
        }
    }
}
