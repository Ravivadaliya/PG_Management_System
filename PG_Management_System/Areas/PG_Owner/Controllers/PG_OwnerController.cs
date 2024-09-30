using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using System.Data;

namespace PG_Management_System.Areas.PG_Owner.Controllers;

[CheckAccess]
[Area("PG_Owner")]
[Route("Owner")]
public class PG_OwnerController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_OwnerController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    [HttpGet("DashBoard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpGet("AdminRegistration")]
    public IActionResult AdminRegistration()
    {
        TempData["AddAdmin"] = null;
        return View();
    }

    [HttpPost("AdminInsert")]
    public IActionResult InsertAdmin(Owner owner)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Please correct the errors in the form.";
                TempData["AlertType"] = "error";
                return View("AdminRegistration");
            }

            OwnerDal ownerDal = new OwnerDal();

            if (ownerDal.InsertAdminData(owner, _dbHelper))
            {
                TempData["AddAdmin"] = "Registration Successful";
                TempData["AlertType"] = "success";
                return RedirectToAction("Login","Login",new { Areas = "Login" });
            }
            else
            {
                TempData["AddAdmin"] = "failed to regestration";
                TempData["AlertType"] = "error";
                return View("AdminRegistration");
            }

        }
        catch (Exception ex)
        {
            TempData["AddAdmin"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return View("AdminRegistration");
        }
    }

    //[HttpPost("Login")]
    //public IActionResult Login(string email, string password)
    //{
    //    try
    //    {
    //        OwnerDal ownerDal = new OwnerDal();
    //        string errorMessage;
    //        DataTable dataTable = ownerDal.SelectOwnerByEmailPass(_dbHelper, email, password);

    //        if (dataTable == null)
    //        {
    //            TempData["AlertType"] = "error";
    //            return View("Login");
    //        }

    //        if (dataTable.Rows.Count > 0)
    //        {
    //            // Logic for successful login
    //            return RedirectToAction("Dashboard");
    //        }
    //        else
    //        {
    //            TempData["Message"] = "Invalid email or password.";
    //            TempData["AlertType"] = "error";
    //            return View("Login");
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
    //        TempData["AlertType"] = "error";
    //        return View("Login");
    //    }
    //}


}
