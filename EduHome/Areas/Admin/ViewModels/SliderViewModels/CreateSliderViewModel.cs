using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.SliderViewModels
{
    public class CreateSliderViewModel 
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(300)]
        public string Description { get; set; }
        [Required]
        public IFormFile ?Img { get; set; }
    }
}
