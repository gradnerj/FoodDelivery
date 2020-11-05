using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests {
    [TestClass]
    public class MenuItemTests {

        private IConfigurationRoot _configuration;
        private DbContextOptions<ApplicationDbContext> _options;
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            var context = new ApplicationDbContext(_options);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        public void UpdateMenuItemTest() {
            MenuItem m1 = new MenuItem() {
                Name = "TestMenuItem",
                Description = "Test Description",
                Image = "",
                Price = (float)5.5,
                CategoryId = 5,
                FoodTypeId = 5,
            };

            _unitOfWork.MenuItem.Add(m1);
            _unitOfWork.Save();

            m1.Price = (float)6.66;

            _unitOfWork.MenuItem.Update(m1);
            _unitOfWork.Save();

            var objFromDb = _unitOfWork.MenuItem.GetFirstorDefault(m => m.Name == "TestMenuItem");

            Assert.IsTrue(objFromDb.Price == (float)6.66);

        }
    }
}
