using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using System.Data;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Hostel.Data;

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
            TempData["Message"] = $"A error occurred";
            TempData["AlertType"] = "error";
            return Json(null);
        }
    }

    public IActionResult AdminRegistration()
    {
        TempData["AddAdmin"] = null;
        return View();
    }

    [HttpPost]
    public IActionResult GetPersonsByNameMobileNumber(string SearchInput)
    {
        if (string.IsNullOrEmpty(SearchInput))
        {
            return BadRequest("Mobile number is required.");
        }
        try
        {
            PersonDal personDal = new PersonDal();
            List<Person> persons = personDal.GetPersonByNameMobile(_dbHelper, SearchInput);
            return Ok(persons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
    [HttpPost]
    public IActionResult GetRoomsByName(string roomSearchInput)
    {
        if (string.IsNullOrEmpty(roomSearchInput))
        {
            return BadRequest("PG number is required.");
        }
        try
        {
            HostelDal hostelDal = new HostelDal();
            List<Hostel> personsWithBed = hostelDal.GetRoomsByName(_dbHelper, roomSearchInput);
            return Ok(personsWithBed);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


}
