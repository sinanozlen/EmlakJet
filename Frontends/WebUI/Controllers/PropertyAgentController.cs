using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class PropertyAgentController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v0 = "Ekibimiz";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Sayfalar";
            ViewBag.v3 = "Çalışma Arkadaşlarımız";
            return View();
        }
    }
}
