using System.ComponentModel.DataAnnotations;

namespace EduHome.Areas.Admin.ViewModels.SliderViewModels
{
	public class UpdateSliderViewModel
	{
		public int Id { get; set; }
	
		public IFormFile? Img { get; set; }
			[Required]
			
		public string Title { get; set; }
		[Required]
		public string Description { get; set; }
	}
}
