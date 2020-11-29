using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models {
    public class Category {
        
        [Key]
        [Display(Name="Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name="Category Name")]
        public string Name { get; set; }

        [Display(Name="Display Order")]
        public int DisplayOrder { get; set; }

    }
}
