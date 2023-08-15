using EduHome.Models.common;

namespace EduHome.Models
{
	public class Events :BaseSectionEntity
	{
		public bool IsDeleted { get; set; }
		public string Title { get; set; }
		public string  Image { get; set; }
		public string Description { get; set; }
		public string Name { get; set; }
		public string Time { get; set; }
		public string Venue { get; set; }
		public string EventTime { get; set; }
     
        public ICollection<SpeakersEvent>? SpeakersEvents { get; set; }

    }

}
