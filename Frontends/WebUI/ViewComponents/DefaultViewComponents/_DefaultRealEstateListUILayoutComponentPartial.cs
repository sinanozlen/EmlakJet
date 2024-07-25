using Microsoft.AspNetCore.Mvc;

namespace WebUI.ViewComponents.DefaultViewComponents
{
    public class _DefaultRealEstateListUILayoutComponentPartial:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
