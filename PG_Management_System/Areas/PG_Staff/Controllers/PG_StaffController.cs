using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
namespace PG_Management_System.Areas.PG_Staff.Controllers;



[Area("PG_Staff")]
[Route("PG_Staff/[controller]/[action]")]
public class PG_StaffController : Controller
{
    private IConfiguration _configuration;
    public PG_StaffController(IConfiguration configuration)
    {
        this._configuration = configuration;
    }

    public IActionResult Index()
    {
        return View();
    }


    public IActionResult AddEditPG_Staff()
    {
        return View();
    }

    public IActionResult AllStaffList()
    {
        return View();
    }
    public IActionResult StaffProfile()
    {
        return View();
    }

    public IActionResult DashBoard()
    {
        return View();
    }
}
