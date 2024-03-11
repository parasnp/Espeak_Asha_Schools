using Microsoft.AspNetCore.Mvc;

namespace ParasIspeak.Controllers
{
    public class AdminDashController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
