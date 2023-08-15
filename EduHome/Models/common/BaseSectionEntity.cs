namespace EduHome.Models.common
{
	public class BaseSectionEntity :BaseEntity
	{
		

		public DateTime CreatedDate { get; set; }

		public DateTime UpdatedDate { get; set; }
		public string CreatedBy { get; set; }

		public string UpdatedBy { get; set; }
	}
}
