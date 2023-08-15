using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.Areas.Admin.Controllers
{
    public class DashBoardController : Controller
    {
        [Area("Admin")]
       
        public IActionResult Index()
        {
            return View();
        }
    }
}
