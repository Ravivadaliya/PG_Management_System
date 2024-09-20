using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Person.Controllers;


[Area("PG_Person")]
[Route("PG_Person/[controller]/[action]")]
public class PG_PersonController : Controller
{

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult AllPersonList()
    {
        return View();
    }
    public IActionResult AddEditPerson()
    {
        return View();
    }
}
