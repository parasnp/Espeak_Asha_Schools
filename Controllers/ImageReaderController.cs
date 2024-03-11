using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Xml;

namespace ParasIspeak.Controllers
{
    public class ImageReaderController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create()
        {
            if (ModelState.IsValid)
            {


            }
            return View();

        }
    }
}
