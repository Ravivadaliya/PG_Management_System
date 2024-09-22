using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Owner.Controllers;


[Area("PG_Owner")]
[Route("PG_Owner/[controller]/[action]")]
public class PG_OwnerController : Controller
{
    private IConfiguration configuration;
    private readonly DatabaseHelper _dbHelper;

    
    public PG_OwnerController(DatabaseHelper dbHelper)  {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }


    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AdminRegistration()
    {
        return View();
    }
    public IActionResult AdminLogin()
    {
        return View();
    }

    public IActionResult InsertAdmin(Owner owner)
    {
        OwnerDal ownerDal = new OwnerDal();
        if (ownerDal.InserAdminData(owner, _dbHelper))
        {
            TempData["AddAdmin"] = "Registration Successfully"; 
        }

        return View("AdminRegistration");
    }
}
