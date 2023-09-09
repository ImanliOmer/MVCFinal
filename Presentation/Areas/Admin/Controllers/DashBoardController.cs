using Microsoft.AspNetCore.Mvc;

namespace Presentation.Areas.Admin.Controllers
{
	[Area("admin")]
	public class DashBoardController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
