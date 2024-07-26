using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.v0 = "Kategoriler";
            ViewBag.v1 = "Ana Sayfa";
            ViewBag.v2 = "Sayfalar";
            ViewBag.v3 = "Kategoriler";
            return View();
        }
    }
}
