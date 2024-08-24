using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Bullky_webRazor_Temp.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        [DisplayName("Category Name")]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1, 100, ErrorMessage = "عدد شما گمتر از حد معمول است ")]
        public int DisplayOrder { get; set; }
    } 
}
