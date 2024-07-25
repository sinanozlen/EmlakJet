using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v0 = "Hakkımızda";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Sayfalar";
            ViewBag.v3 = "Hakkımızda";
            return View();
        }
    }
}
