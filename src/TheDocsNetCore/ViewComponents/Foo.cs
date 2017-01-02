using Microsoft.AspNetCore.Mvc;

namespace TheDocsNetCore.ViewComponents
{
    public class Foo : ViewComponent
    {
        public IViewComponentResult Invoke ()
        {
            return View();
        }
    }
}
