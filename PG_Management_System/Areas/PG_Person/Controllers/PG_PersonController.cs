using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Person.Controllers;


[Area("PG_Person")]
[Route("PG_Person/[controller]/[action]")]
public class PG_PersonController(DatabaseHelper dbHelper) : Controller
{
    public IConfiguration Configuration;
    private readonly DatabaseHelper _dbHelper = dbHelper;

    public IActionResult AllPersonList()
    {
        PersonDal personDal = new PersonDal();
        DataTable dataTable = personDal.GetAllPersonByOwnerId(_dbHelper);
        return View("AllPersonList",dataTable);
    }
    public IActionResult AddEditPerson()
    {
        return View();
    }
    public IActionResult Add(int? Id)
    {
        PersonDal personDal = new PersonDal();
        int Owner_Id =Convert.ToInt32(CV.Owner_Id());

        ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);

        ViewBag.RoomList = new List<Room_DropDownModel>();
        ViewBag.BedList = new List<Bed_DropDownmodel>();
        return View("AddEditPerson");
    }


    #region PG_Room_RoomDropDownByHostelId
    public IActionResult PG_Room_RoomDropDownByHostelId(int Hostel_Id)
    {
        PersonDal personDal = new PersonDal();
        ViewBag.RoomList = personDal.GetRoomListByHostelId(_dbHelper, Hostel_Id);
        var roomModle = ViewBag.RoomList;
        return Json(roomModle);
    }
    #endregion

    public IActionResult PG_Bed_BedDropDownByRoomId(int Room_ID)
    {
        PersonDal personDal = new PersonDal();
        ViewBag.BedList = personDal.GetBedListByRoomID(_dbHelper, Room_ID);
        var BedModle = ViewBag.BedList;
        return Json(BedModle);
    }

}
