using EduHome.Models.common;

namespace EduHome.Models
{
	public class SpeakersEvent : BaseEntity
	{
		public int SpeakersId { get; set; }
		public Speakers Speakers { get; set; }
		public int EventsId { get; set; }
		public Events  Events { get; set; }
	}
}
