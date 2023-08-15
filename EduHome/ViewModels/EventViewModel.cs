using EduHome.Models;

namespace EduHome.ViewModels
{
    public class EventViewModel
    {
        public IEnumerable<Speakers> Speakers { get; set; }
       public IEnumerable<Events> Events{ get; set; }
    }
}
