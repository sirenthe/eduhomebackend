
using EduHome.Areas.Admin.ViewModels.SliderViewModels;
using EduHome.Contexts;
using EduHome.Models;
using EduHome.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EduHome.Areas.Admin.Controllers
{
  
    [Area("Admin")]
    public class SliderController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SliderController(AppDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
       
        public async Task<IActionResult> Index()
        {

            var sliders = await _context.Sliders.Where(s => s.IsDeleted == false).ToListAsync();

            return View(sliders);
        }

        public async Task<IActionResult> Create()

        {
            if (await _context.Sliders.CountAsync(s => !s.IsDeleted) == 3)
            {
                return BadRequest();
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSliderViewModel createSliderViewModel)
        {

            if (await _context.Sliders.CountAsync(s => !s.IsDeleted) == 3)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return View();
            if(!createSliderViewModel.Img.CheckFileType("image/"))
            {
                ModelState.AddModelError("Img", "sekil daxil edin");
                return View();
            }
            if (!createSliderViewModel.Img.CheckFileSize(2))
            {
                ModelState.AddModelError("Img", "seklin olcusu uygun deyil")
                    ; return View();
            }
            string fileName = $"{Guid.NewGuid()}-{createSliderViewModel.Img.FileName}";

            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "slider", fileName);

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                await createSliderViewModel.Img.CopyToAsync(fileStream);
            }







            Slider slider = new Slider
            {
                Title = createSliderViewModel.Title,
            Img=fileName,
                Description = createSliderViewModel.Description,
            };
            await _context.Sliders.AddAsync(slider);
            await _context.SaveChangesAsync();  
            return RedirectToAction(nameof(Index));
        }









        public async Task<IActionResult> Update(int id)
        {
           var slider= await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            if(slider is null)
            {
                return NotFound();
            }
            UpdateSliderViewModel updateSliderViewModel = new UpdateSliderViewModel
            {
                Title = slider.Title,
  
                Description = slider.Description,
                Id = slider.Id
            };
            return View(updateSliderViewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]







        public async Task<IActionResult> Update(UpdateSliderViewModel updateSliderViewModel, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
            if (slider is null)
            {
                return NotFound();
            }


            if (updateSliderViewModel.Img is not null)
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "slider", slider.Img);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                string fileName = $"{Guid.NewGuid()}-{updateSliderViewModel.Img.FileName}";
                string newpath = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "slider", fileName);
                using (FileStream fstream = new FileStream(newpath, FileMode.Create))
                {
                    await updateSliderViewModel.Img.CopyToAsync(fstream);
                }
                slider.Img = fileName;
            }


            slider.Title = updateSliderViewModel.Title;
     
            slider.Description = updateSliderViewModel.Description;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }












        public async Task<IActionResult> Delete(int Id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(s => s.Id == Id && !s.IsDeleted);
            if (slider is null)
            {
                return NotFound();
            }
            DeleteSliderViewModel deleteSliderViewModel = new DeleteSliderViewModel { 
                Description= slider.Description,
            Img= slider.Img,
            Title= slider.Title,
                    

            };
            return View(deleteSliderViewModel);

        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSlider(int id)
        {
            var slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider is null)
                return NotFound();


            string path = Path.Combine(_webHostEnvironment.WebRootPath, "assets", "img", "slider", slider.Img);
            if (System.IO.File.Exists(path))
            {

                System.IO.File.Delete(path);
            }

            //return Content(path);


            slider.IsDeleted=true;

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Detail(int id)
        {
            Slider? slider = await _context.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider is null)
            {
                return NotFound();
            }
            return View(slider);
        }


    }
}
