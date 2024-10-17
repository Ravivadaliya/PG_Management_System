using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.BAL;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Bed.Controllers;

[CheckAccess]
[Area("PG_Bed")]
[Route("Bed")]
public class PG_BedController(DatabaseHelper dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = dbHelper;

    [HttpGet("SelectPG")]
    public IActionResult PGList()
    {
        try
        {
            HostelDal hosteldal = new HostelDal();
            DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);
            return View("PGList", dataTable);
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error loading PG list. Please try again.";
            TempData["AlertType"] = "error";
            return RedirectToAction("Index");
        }
    }

    [HttpGet("Beds")]
    public IActionResult BedListByHostelId(int Hostel_Id)
    {
        try
        {
            BedDal bedDal = new BedDal();
            DataTable dataTable = bedDal.GetAllBedFromHostelId(_dbHelper, Hostel_Id);
            return View("AllBedList", dataTable);
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error loading bed list. Please try again.";
            TempData["AlertType"] = "error";
            return RedirectToAction("PGList");
        }
    }

    [HttpPost("AssignBedToPerson")]
    public IActionResult AssignBedToPerson(int Bed_ID, int Person_ID, string Payment_Cycle, int Deposit, DateOnly Payment_Date)
    {
        BedDal bedDal = new BedDal();
        try
        {
            if (bedDal.insertPersonOnBed(_dbHelper, Bed_ID, Person_ID, Deposit, Payment_Cycle, Payment_Date))
            {
                return Json(new { success = true, message = "Bed assigned successfully." });
            }
            else
            {
                return Json(new { success = false, message = "Error occur while assign bed to person" });
            }
        }
        catch (Exception ex)
        {
            // If an error occurs, return a JSON response indicating failure
            return Json(new { success = false, message = "An error occurred: " + ex.Message });
        }
    }


    [HttpPost("removePersonFromBed")]
    public IActionResult removePersonFromBed(int Bed_Id)
    {
        BedDal bedDal = new BedDal();
        if (bedDal.RemovePersonFromBed(_dbHelper, Bed_Id))
        {

            return Json(new { success = true, message = "Remove Person From Bed!" });

        }
        else
        {

            return Json(new { success = false, message = "Error occur while assign bed to person" });
        }

    }

    [HttpGet("GetPersonsByPartialMobileNumber")]
    public IActionResult GetPersonsByPartialMobileNumber(string partialMobileNumber)
    {
        if (string.IsNullOrEmpty(partialMobileNumber))
        {
            return BadRequest("Mobile number is required.");
        }

        try
        {
            PersonDal personDal = new PersonDal();
            List<Person> persons = personDal.GetPersonIdUsingMobile(_dbHelper, partialMobileNumber);

            return Ok(persons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }


    [HttpGet("AddBed")]
    public IActionResult AddBed(int? Hostel_Id, string PG_Number)
    {
        if (Hostel_Id == null || Hostel_Id == 0)
        {
            TempData["Message"] = "Invalid Hostel ID.";
            return RedirectToAction("PGList");
        }
        try
        {
            PersonDal personDal = new PersonDal();
            ViewBag.RoomList = personDal.GetRoomListByHostelIdForbed(_dbHelper, Hostel_Id);
            ViewBag.PG_Number = PG_Number;
            return View("AddEditBed");
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error loading add bed page. Please try again.";
            TempData["AlertType"] = "error";
            return RedirectToAction("PGList");
        }
    }


    [HttpPost("DeleteBed")]
    public IActionResult DeleteBed(int Bed_Id)
    {
        try
        {
            BedDal bedDal = new BedDal();
            if (bedDal.DeleteBed(_dbHelper, Bed_Id))
            {
                TempData["Message"] = "Bed deleted successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["Message"] = "Error deleting the bed. Please try again.";
                TempData["AlertType"] = "error";
            }
        }
        catch (Exception ex)
        {
            TempData["Message"] = "An error occurred while deleting the bed.";
            TempData["AlertType"] = "error";
        }

        return RedirectToAction("PGList");
    }


    [HttpPost("SaveBed")]
    public IActionResult SaveBed(Bed bed)
    {
        try
        {
            BedDal bedDal = new BedDal();
            if (bedDal.InsertBedData(_dbHelper, bed))
            {
                TempData["Message"] = "Bed data inserted successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to insert bed data.";
                TempData["AlertType"] = "error";
            }
        }
        catch (Exception ex)
        {
            TempData["Message"] = "An error occurred while saving bed data.";
            TempData["AlertType"] = "error";
        }

        return RedirectToAction("PGList");
    }
}
