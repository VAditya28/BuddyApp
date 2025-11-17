using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Buddy.Models.Models
{
    public class Category 
    {
        [Key]
        public int category_Id { get; set; }
        [Required]
        [DisplayName("Category Name")]
        [MaxLength(30, ErrorMessage ="Exceeded length")]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(0, 100,ErrorMessage ="Must be 1 to 100")]
        public int DisplayOrder { get; set; }
    }
}
