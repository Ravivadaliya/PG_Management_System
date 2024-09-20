using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Hostel.Controllers;

[Area("PG_Hostel")]
[Route("PG_Hostel/[controller]/[action]")]
public class PG_HostelController : Controller
{
    private IConfiguration configuration;

    public PG_HostelController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }
    public IActionResult AllPgList()
    {
        return View();
    }

    public IActionResult AddEditPG_Hostel()
    {
        return View();
    }
}
