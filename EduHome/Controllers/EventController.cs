using EduHome.Contexts;
using Microsoft.AspNetCore.Mvc;
using EduHome.Models;
using Microsoft.EntityFrameworkCore;
using EduHome.ViewModels;

namespace EduHome.Controllers
{
    public class EventController : Controller
    {

		private readonly AppDbContext _context;
		public EventController(AppDbContext context)
		{
			_context = context;
		}
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events.Where(e => e.IsDeleted == false).ToListAsync();
            return View(events);

        }
      

        public async Task<IActionResult> Joinnow(int id)
        {

            Events events = await _context.Events.Include(e => e.SpeakersEvents).
         ThenInclude(se => se.Speakers).FirstOrDefaultAsync(e => e.Id == id && !e.IsDeleted);

            if (events == null)
                return NotFound();
            EventViewModel eventViewModel = new EventViewModel
            {



        };





            return View(eventViewModel);
        }

    }
}
