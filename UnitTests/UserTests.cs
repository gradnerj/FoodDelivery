using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests {
    [TestClass]
    public class UserTests {


        private IConfigurationRoot _configuration;
        private DbContextOptions<ApplicationDbContext> _options;
        private readonly IUnitOfWork _unitOfWork;


        public UserTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            var context = new ApplicationDbContext(_options);
            _unitOfWork = new UnitOfWork(context);
        }
        [TestMethod]
        public void UserCreationTest() {
            var TestUser = new ApplicationUser() {
                FirstName = "Testy",
                LastName = "McTesty",
                PhoneNumber = "1234567899",
            };

            var quantityOfAppUsers = _unitOfWork.ApplicationUser.GetAll().Count();
            _unitOfWork.ApplicationUser.Add(TestUser);
            _unitOfWork.Save();
            var newQuantityOfAppUsers = _unitOfWork.ApplicationUser.GetAll().Count();
            Assert.IsTrue(newQuantityOfAppUsers == quantityOfAppUsers + 1);
        }



    }
}
