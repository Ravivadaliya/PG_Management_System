using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Bed.Controllers;

[CheckAccess]
[Area("PG_Bed")]
[Route("PG_Bed/{controller}/{action}")]
public class PG_BedController(DatabaseHelper dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = dbHelper;

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

    [HttpPost]
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

    [HttpGet]
    public IActionResult GetPersonsByPartialMobileNumber(string partialMobileNumber)
    {
        if (string.IsNullOrEmpty(partialMobileNumber))
        {
            return BadRequest("Mobile number is required.");
        }

        try
        {
            List<object> persons = new List<object>();

            string connectionString = "Data Source=MSI\\MSSQLSERVER01;Initial Catalog=PG_ManagementSystem;Integrated Security=true;MultipleActiveResultSets=True;"; // Replace with your actual connection string

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("GetPersonIdByPartialMobileNumber", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PartialMobileNumber", partialMobileNumber);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            persons.Add(new
                            {
                                Person_Id = reader["Id"],
                                MobileNumber = reader["Person_Mobile_Number"]
                            });
                        }
                    }
                }
            }

            return Ok(persons);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }



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
