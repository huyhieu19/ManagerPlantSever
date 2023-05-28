using Microsoft.AspNetCore.Mvc;

namespace ManagerServer.Controllers
{
    public class ZoneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
