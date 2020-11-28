using Infrastructure.Data;
using ApplicationCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using Moq;
namespace UnitTests {
    [TestClass]
    public class FoodTypeTests {
        private readonly IConfigurationRoot _configuration;
        private readonly DbContextOptions<ApplicationDbContext> _options;
        private readonly ApplicationDbContext _context;
        
        
        
        public List<string> CleanUpList { get; set; }



        public FoodTypeTests() {
            var builder = new ConfigurationBuilder().SetBasePath(System.IO.Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");
            _configuration = builder.Build();
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(_configuration.GetConnectionString("ApplicationDbContext")).Options;
            _context = new ApplicationDbContext(_options);
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

            if(!_context.FoodType.Any(f => f.Name == f1.Name)) {
                CleanUpList.Add("TypeTest");
                _context.FoodType.Add(f1);
                _context.SaveChanges();
            }

            

            FoodType f2 = new FoodType() {
                Name = "TypeTest"
            };
            if (!_context.FoodType.Any(f => f.Name == f2.Name)) {
                CleanUpList.Add("TypeTest");
                _context.FoodType.Add(f2);
                _context.SaveChanges();
                Assert.Fail("Duplicate Food Types should not be entered");
            }
            CleanUp();
            
        }
    }
}
