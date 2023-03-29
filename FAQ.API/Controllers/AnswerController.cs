using Microsoft.AspNetCore.Mvc;

namespace FAQ.API.Controllers
{
    public class AnswerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
