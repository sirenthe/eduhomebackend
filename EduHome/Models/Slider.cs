using System.ComponentModel.DataAnnotations;
using EduHome.Models.common;

namespace EduHome.Models
{
    public class Slider :BaseEntity
    {
       
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public string Img { get; set; } 
        public bool IsDeleted { get; set; }
    }
}
