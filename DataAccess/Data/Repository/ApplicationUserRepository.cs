using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;

namespace FoodDelivery.DataAccess.Data.Repository {
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context) : base(context) {
            _context = context;
        }


             

    }
}
