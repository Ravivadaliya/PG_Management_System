using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Bed.Data;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Room.Data;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Bed.Controllers;


[Area("PG_Bed")]
[Route("PG_Bed/[controller]/[action]")]
public class PG_BedController(DatabaseHelper dbHelper) : Controller
{

    private readonly DatabaseHelper _dbHelper = dbHelper;
    public IActionResult Index()
    {
        return View();
    }
    public IActionResult PGList()
    {
        HostelDal hosteldal = new HostelDal();
        DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);

        return View("PGList", dataTable);
    }

    public IActionResult bedListByHostelId(int Hostel_Id)
    {
        TempData.Remove("Message");
        TempData.Remove("AlertType");

        BedDal bedDal = new BedDal();
        DataTable dataTable = bedDal.GetAllBedFromHostelId(_dbHelper,Hostel_Id);
        return View("AllBedList", dataTable);
    }

    public IActionResult Add(string Hostel_Id)
    {
        TempData["HostelId"] = Hostel_Id;
        return View("AddEditBed");
    }

    public IActionResult AddBed(int Hostel_Id)
    {
        //use person dal for reoccurance 
        PersonDal personDal = new PersonDal();

        ViewBag.RoomList = personDal.GetRoomListByHostelId(_dbHelper, Hostel_Id);
        return View("AddEditBed");
    }

    public IActionResult Deletebed(int Bed_Id)
    {
        BedDal bedDal = new BedDal();
        if (bedDal.DeleteBed(_dbHelper,Bed_Id))
        {
            TempData["Message"] = "Bed Delete successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            TempData["Message"] = "Error to Delete bed!";
            TempData["AlertType"] = "success";
        }
        return RedirectToAction("PGList");
    }

    public IActionResult SaveBed(Bed bed)
    {
        BedDal bedDal = new BedDal();   
        if (bedDal.InsertRoomData(_dbHelper, bed))
        {
            TempData["Message"] = "Bed Data Insert successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            TempData["Message"] = "Failed To Insert Bed Data.";
            TempData["AlertType"] = "Error";
        }
        return RedirectToAction("PGList");
    }

}
