using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class FAQController : Controller
    {
        public FAQController()
        {
            
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
