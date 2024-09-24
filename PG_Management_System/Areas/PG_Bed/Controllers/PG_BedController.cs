using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Bed.Controllers
{
    public class PG_BedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
