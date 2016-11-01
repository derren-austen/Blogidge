using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace TheDocsNetCore.Controllers
{
    [Authorize(Policy = "AllPoweridge")]
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}