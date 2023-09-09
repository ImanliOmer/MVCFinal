using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.OurVisionGoal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OurVisionGoalController : Controller
    {
        private readonly IOurVisionGoalService _ourvisionGoalService;

        public OurVisionGoalController(IOurVisionGoalService visionGoalService)
        {
            _ourvisionGoalService = visionGoalService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _ourvisionGoalService.GetAllAsync();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(OurVisionGoalCreateVM model)
        {
            var isSucceded = await _ourvisionGoalService.CreateAsync(model);
            if (isSucceded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int id)
        {
            var model = await _ourvisionGoalService.UpdateAsync(id);
            if (model is null) return NotFound();

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync(OurVisionGoalUpdatedVM model, int id)
        {
            var isSucceeded = await _ourvisionGoalService.UpdateAsync(model, id);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var isSucceeded = await _ourvisionGoalService.DeleteAsync(id);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return NotFound("Vision not found");
        }

    }
}
