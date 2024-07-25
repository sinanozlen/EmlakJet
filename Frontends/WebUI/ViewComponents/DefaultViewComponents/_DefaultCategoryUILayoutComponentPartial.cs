using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultCategoryUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
