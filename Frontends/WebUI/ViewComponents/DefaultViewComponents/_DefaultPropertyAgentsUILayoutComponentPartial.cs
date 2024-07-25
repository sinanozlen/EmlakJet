using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultPropertyAgentsUILayoutComponentPartial: ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
