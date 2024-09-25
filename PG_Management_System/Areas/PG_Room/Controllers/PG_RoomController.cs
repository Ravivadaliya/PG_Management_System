using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Room.Data;
using PG_Management_System.Areas.PG_Person.Data;
using System.Data;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Staff.Data;
using PG_Management_System.Areas.PG_Staff.Models;
using PG_Management_System.Areas.PG_Room.Models;

namespace PG_Management_System.Areas.PG_Room.Controllers;

[Area("PG_Room")]
[Route("PG_Room/[controller]/[action]")]
public class PG_RoomController(DatabaseHelper dbHelper) : Controller
{
    public IConfiguration Configuration;
    private readonly DatabaseHelper _dbHelper = dbHelper;

    public IActionResult PGList()
    {
        HostelDal hosteldal = new HostelDal();
        DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);
        return View("PGList", dataTable);
    }
    public IActionResult AllRoomListByHostelId(string Id)
    {
        RoomDal roomDal = new RoomDal();
        DataTable dataTable = roomDal.GetAllRoomByHostelId(_dbHelper, Id);
        return View("AllRoomList", dataTable);
    }
    
    public IActionResult Index()
    {
        return View();
    }

    //public IActionResult EditRoom(int? Id)
    //{
    //    if (Id == null)
    //    {

    //    }
    //}

    public IActionResult Add(int? Id,int Hostel_Id)
    {
        if (Id != null)
        {
            RoomDal roomDal = new RoomDal();
            DataTable dataTable = roomDal.GetRoomById(_dbHelper, Id);

            Room room = new Room();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    ViewBag.HostelId = Convert.ToInt32(dr["Hostel_ID"]);
                    room.Hostel_ID = Convert.ToInt32(dr["Hostel_ID"]);
                    room.Room_Number = dr["Room_Number"].ToString().Split('-')[0];
                    room.Room_GenderAllowed= dr["Room_GenderAllowed"].ToString();
                    room.Room_SharingType = Convert.ToInt32(dr["Room_SharingType"]);
                    room.Room_AllowcateBed = Convert.ToInt32(dr["Room_AllowcateBed"]);
                    room.Room_Type = dr["Room_Type"].ToString();
                }
            }
            return View("AddEditRoom", room);
        }

        TempData["HostelId"] = Hostel_Id;
        return View("AddEditRoom");
    }

    public IActionResult SaveStaff(Room room)
    {
        RoomDal roomDal = new RoomDal();
        if (room.Id == null)
        {

            if (roomDal.InsertRoomData(_dbHelper, room))
            {
                TempData["Message"] = "Room added successfully!";
                TempData["AlertType"] = "success"; 
            }
            else
            {
                TempData["Message"] = "Failed to add Room.";
                TempData["AlertType"] = "error"; 
            }
        }
        else
        {
            if (roomDal.UpdateRoomData(_dbHelper, room))
            {
                TempData["Message"] = "Room Data Update successfully!";
                TempData["AlertType"] = "success"; 
            }
            else
            {
                TempData["Message"] = "Failed To Update Staff Data.";
                TempData["AlertType"] = "Error";
            }
            return RedirectToAction("PGList");
        }

        return RedirectToAction("PGList");
    }

    public IActionResult DeleteRoom(int Id)
    {
        RoomDal roomDal = new RoomDal();
        if (roomDal.Deleteroom(_dbHelper, Id))
        {
            TempData["Message"] = "Room Delete successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            TempData["Message"] = "Error to Delete";
            TempData["AlertType"] = "error";
        }
        return RedirectToAction("PGList");
    }
}
