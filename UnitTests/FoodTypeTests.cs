using FoodDelivery.Data;
using FoodDelivery.DataAccess.Data.Repository;
using FoodDelivery.DataAccess.Data.Repository.IRepository;
using FoodDelivery.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
namespace UnitTests {
    [TestClass]
    public class FoodTypeTests {
        private IConfigurationRoot _configuration;
        private DbContextOptions<ApplicationDbContext> _options;
       // private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;
        
        
        
        public List<string> CleanUpList { get; set; }



        public FoodTypeTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            _context = new ApplicationDbContext(_options);
           // _unitOfWork = new UnitOfWork(_context);

        }


        public void CleanUp() {
            foreach(var val in CleanUpList) {
                var objToDelete = _context.FoodType.Where(m => m.Name == val);
                foreach(var obj in objToDelete) {
                    _context.FoodType.Remove(obj);
                }
               
            }
            _context.SaveChanges();
        }

        [TestMethod]
        public void DuplicateFoodTypeTest() {
            CleanUpList = new List<string>();
            FoodType f1 = new FoodType() {
                Name = "TypeTest"
            };
            CleanUpList.Add("TypeTest");
            _context.FoodType.Add(f1);
            _context.SaveChanges();

            FoodType f2 = new FoodType() {
                Name = "TypeTest"
            };

            CleanUpList.Add("TypeTest");
            _context.FoodType.Add(f2);
            _context.SaveChanges();

            CleanUp();
            Assert.Fail("An exception should have thrown for adding a Food Type that already exists.");
        }
    }
}
