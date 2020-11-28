using Infrastructure.Data;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests {
    [TestClass]
    public class UserTests {


        private readonly IConfigurationRoot _configuration;
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;

        public UserTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            _context = new ApplicationDbContext(_options);
        }

        public List<string> CleanUpList { get; set; }
        public void CleanUp() {
            foreach(var email in CleanUpList) {
                var objToDelete = _context.ApplicationUser.FirstOrDefault(a => a.Email == email);
                _context.ApplicationUser.Remove(objToDelete);
            }
            _context.SaveChanges();
        }
        [TestMethod]
        public void UserCreationTest() {
            CleanUpList = new List<string>();
            var TestUser = new ApplicationUser() {
                Email = "Testy@testy.com",
                FirstName = "Testy",
                LastName = "McTesty",
                PhoneNumber = "1234567899",
            };
            CleanUpList.Add("Testy@testy.com");
            var quantityOfAppUsers = _context.ApplicationUser.ToList().Count();
            _context.ApplicationUser.Add(TestUser);
            _context.SaveChanges();
            var newQuantityOfAppUsers = _context.ApplicationUser.ToList().Count();

            CleanUp();
            Assert.IsTrue(newQuantityOfAppUsers == quantityOfAppUsers + 1);
        }



    }
}
