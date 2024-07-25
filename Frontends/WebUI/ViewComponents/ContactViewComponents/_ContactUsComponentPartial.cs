using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.ContactViewComponents
{
    public class _ContactUsComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
