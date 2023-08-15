using EduHome.Contexts;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;
        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var sliders = _context.Sliders.Where(s=>s.IsDeleted==false);
            return View(sliders);
        }
    }
}
