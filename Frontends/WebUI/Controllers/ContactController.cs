using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v0 = "Bizimle İletişime Geçin";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Sayfalar";
            ViewBag.v3 = "İletişim";
            return View();
        }
    }
}
