using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests {
    [TestClass]
    public class MenuItemTests {

        private IConfigurationRoot _configuration;
        private DbContextOptions<ApplicationDbContext> _options;
        //private readonly IUnitOfWork _unitOfWork;
        private ApplicationDbContext _context;

        public List<string> CleanUpList { get; set; }

        public MenuItemTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            _context = new ApplicationDbContext(_options);
           // _unitOfWork = new UnitOfWork(_context);
        }
         public void CleanUp() {
            var objToRemove = _context.MenuItem.Where(m => m.Name == CleanUpList[0]);
            _context.MenuItem.Remove(objToRemove.FirstOrDefault());
            _context.SaveChanges();
        }

        [TestMethod]
        public void UpdateMenuItemTest() {

            CleanUpList = new List<string>();

            MenuItem m1 = new MenuItem() {
                Name = "TestMenuItem",
                Description = "Test Description",
                Image = "",
                Price = (float)5.5,
                CategoryId = 5,
                FoodTypeId = 5,
            };

            _context.MenuItem.Add(m1);
            _context.SaveChanges();

            m1.Price = (float)6.66;
            CleanUpList.Add("TestMenuItem");
            _context.MenuItem.Update(m1);
            _context.SaveChanges();

            var objFromDb = _context.MenuItem.FirstOrDefault(m => m.Name == "TestMenuItem");
            CleanUp();
            Assert.IsTrue(objFromDb.Price == (float)6.66);

        }
    }
}
