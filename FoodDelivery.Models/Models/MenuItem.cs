using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodDelivery.Models {
    public class MenuItem {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Menu Item")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Price Should be greater than $1")]
        public float Price { get; set; }

        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [Display(Name = "Food Type")]
        public int FoodTypeId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("FoodTypeId")]
        public virtual FoodType FoodType { get; set; }
    }
}
