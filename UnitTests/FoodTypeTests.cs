using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests {
    [TestClass]
    public class FoodTypeTests {
        private IConfigurationRoot _configuration;
        private DbContextOptions<ApplicationDbContext> _options;
        private readonly IUnitOfWork _unitOfWork;

        public FoodTypeTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            var context = new ApplicationDbContext(_options);
            _unitOfWork = new UnitOfWork(context);
        }

        [TestMethod]
        public void DuplicateFoodTypeTest() {
            FoodType f1 = new FoodType() {
                Name = "TypeTest"
            };

            _unitOfWork.FoodType.Add(f1);
            _unitOfWork.Save();

            FoodType f2 = new FoodType() {
                Name = "TypeTest"
            };
            _unitOfWork.FoodType.Add(f2);
            _unitOfWork.Save();
            Assert.Fail("An exception should have thrown for adding a Food Type that already exists.");
        }
    }
}
