using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultAgentInfoUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
