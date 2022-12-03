using Microsoft.AspNetCore.Mvc;

using UserActionTrackingApp.Models;

namespace UserActionTrackingApp.Controllers
{
    public class OtherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
