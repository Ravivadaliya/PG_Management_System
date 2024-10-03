using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using System.Data;
using PG_Management_System.Areas.PG_Person.Data;

namespace PG_Management_System.Areas.PG_Owner.Controllers;

[CheckAccess]
[Area("PG_Owner")]
[Route("PG_Owner/{controller}/{action}")]
public class PG_OwnerController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_OwnerController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult SelectPGDetalsForDashBoard()
    {
        try
        {
            OwnerDal personDal = new OwnerDal();
            ViewBag.DashBoardDetails = personDal.PGDetalsForDashBoard(_dbHelper);
            var dashboardModel = ViewBag.DashBoardDetails;
            return Json(dashboardModel);
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred";
            TempData["AlertType"] = "error";
            return Json(null);
        }
    }

    public IActionResult AdminRegistration()
    {
        TempData["AddAdmin"] = null;
        return View();
    }
}
