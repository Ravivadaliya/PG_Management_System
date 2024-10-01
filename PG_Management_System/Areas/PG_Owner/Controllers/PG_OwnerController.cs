using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using System.Data;

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

    public IActionResult AdminRegistration()
    {
        TempData["AddAdmin"] = null;
        return View();
    }
}
