using Microsoft.AspNetCore.Mvc;

namespace FAQ.API.Controllers
{
    public class TagController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
