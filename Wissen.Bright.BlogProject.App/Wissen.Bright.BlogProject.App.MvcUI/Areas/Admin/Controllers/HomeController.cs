using Microsoft.AspNetCore.Mvc;

namespace Wissen.Bright.BlogProject.App.WebMvcUI.Areas.Admin.Controllers
{        
    [Area("Admin")]
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserList()
        {
            return View();
        }
    }
}
