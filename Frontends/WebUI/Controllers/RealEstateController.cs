using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class RealEstateController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v0 = "Tüm Taşınmazlarımız";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Sayfalar";
            ViewBag.v3 = "Tüm Taşınmazlar";
            return View();
        }
    }
}
