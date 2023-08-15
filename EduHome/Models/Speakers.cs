using EduHome.Models.common;

namespace EduHome.Models
{
	public class Speakers :BaseEntity
	{
		public bool IsDeleted { get; set; }
		public string? Name { get; set; }
		public string? Occupation { get; set; }
			public string? Image { get ; set; }
     

    }
}
