using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

//!  An ApplicationUser class. Inherits from IdentityUser 
namespace ApplicationCore.Models {
    public class ApplicationUser : IdentityUser {
        //! FirstName
        public string FirstName { get; set; }
        //! LastName
        public string LastName { get; set; }
        //! FullName: Not Mapped
        [NotMapped]
        public string FullName { get { return FirstName + " " + LastName; } }
    }
}