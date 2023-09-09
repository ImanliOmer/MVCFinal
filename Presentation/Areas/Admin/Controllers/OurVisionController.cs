using Business.Services.Abstract.Admin;
using Business.ViewModels.Admin.OurVision;
using DataAccess.Migrations;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	public class OurVisionController : Controller
	{
		[Area("Admin")]
		public class VisionController : Controller
		{
			private readonly IOurVisionService _ourVisionService;

			public VisionController(IOurVisionService ourVisionService)
			{
				_ourVisionService = ourVisionService;
			}

			[HttpGet]
			public async Task<IActionResult> Index()
			{
				var model = await _ourVisionService.GetAllAsync();
				return View(model);
			}

			[HttpGet]
			public async Task<IActionResult> UpdateAsync(int id)
			{
				var model = await _ourVisionService.UpdateAsync(id);
				if (model is null) return NotFound();

				return View(model);
			}

			[HttpPost]
			public async Task<IActionResult> UpdateAsync(OurVisionUpdateVM model, int id)
			{
				var isSucceeded = await _ourVisionService.UpdateAsync(model, id);
				if (isSucceeded) return RedirectToAction(nameof(Index));

				return View(model);
			}

			

		}
	}
}