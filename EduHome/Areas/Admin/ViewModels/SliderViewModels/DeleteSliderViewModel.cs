using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.SliderViewModels
{
    public class DeleteSliderViewModel
    {

        public int Id { get; set; }
        [Required]
        public string Img { get; set; }
        [Required]

        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
