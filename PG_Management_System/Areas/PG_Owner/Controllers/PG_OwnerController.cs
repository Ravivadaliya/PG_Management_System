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
using PG_Management_System.Areas.PG_Payments.Models;
using PG_Management_System.Areas.PG_Payments.Data;

namespace PG_Management_System.Areas.PG_Owner.Controllers;

[CheckAccess]
[Area("PG_Owner")]
[Route("PGOwner")]
public class PG_OwnerController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_OwnerController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    [HttpGet("Dashboard")]
    public IActionResult Dashboard()
    {
        return View();
    }

    [HttpPost("SelectPGDetalsForDashBoard")]
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

    [HttpGet("AdminRegistration")]
    public IActionResult AdminRegistration()
    {
        TempData["AddAdmin"] = null;
        return View();
    }

    [HttpPost("GetPersonsByNameMobileNumber")]
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

    [HttpPost("GetRoomsByName")]
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

    [HttpGet("GetPendingPayments")]
    public IActionResult GetPendingPayments()
    {
        try
        {
            PaymentsDal paymentsDal = new PaymentsDal();
            List<PendingPaymentViewModel> paymentViewModels = paymentsDal.GetPenddingPaymentListByOwnerId(_dbHelper);
            return Ok(paymentViewModels);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }

    [HttpPost("GetMonthlyPayments")]
    public IActionResult GetMonthlyPayments()
    {
        try
        {
            PaymentsDal paymentsDal = new PaymentsDal();
            DataTable dataTable = paymentsDal.GetMothlyAndyealyPayment(_dbHelper);

            // Convert the DataTable to a list of dynamic objects or a strongly typed list
            var payments = dataTable.AsEnumerable().Select(row => new
            {
                PaymentYear = row.Field<int>("PaymentYear"),
                PaymentMonth = row.Field<int>("PaymentMonth"),
                TotalPaymentAmount = row.Field<int>("TotalPaymentAmount")
            }).ToList();

            return Json(payments);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error fetching payment data." });
        }
    }
}
