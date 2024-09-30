using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Bed.Controllers;

[CheckAccess]
[Area("PG_Bed")]
[Route("Bed")]
public class PG_BedController(DatabaseHelper dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = dbHelper;

    [HttpGet("PGList")]
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

    [HttpGet("BedList")]
    public IActionResult BedListByHostelId(int Hostel_Id)
    {
        try
        {
            TempData.Remove("Message");
            TempData.Remove("AlertType");

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

    [HttpGet("AddBed")]
    public IActionResult AddBed(int Hostel_Id)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            ViewBag.RoomList = personDal.GetRoomListByHostelIdForbed(_dbHelper, Hostel_Id);
            return View("AddEditBed");
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error loading add bed page. Please try again.";
            TempData["AlertType"] = "error";
            return RedirectToAction("PGList");
        }
    }

    [HttpPost("Deletebed")]
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

    [HttpPost("SavePg")]
    public IActionResult SaveBed(Bed bed)
    {
        try
        {
            BedDal bedDal = new BedDal();
            if (bedDal.InsertRoomData(_dbHelper, bed))
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
