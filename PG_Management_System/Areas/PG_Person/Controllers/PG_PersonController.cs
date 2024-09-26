using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
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
        return View("AllPersonList", dataTable);
    }
    public IActionResult AddEditPerson()
    {
        return View();
    }
    public IActionResult Add(int? Person_Id)
    {
        PersonDal personDal = new PersonDal();
        if (Person_Id != null)
        {
            DataTable dataTable = personDal.GetPersonById(_dbHelper, Person_Id);

            Person person = new Person();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    ViewBag.HostelList = new List<Hostel_DropDownModel>();
                    ViewBag.RoomList= new List<Hostel_DropDownModel>();
                    //ViewBag.Bedlist = new List<Hostel_DropDownModel>();
                    person.Id = Convert.ToInt32(dr["Id"]);
                    person.Bed_ID = Convert.ToInt32(dr["Bed_Id"]);
                    person.Room_ID = Convert.ToInt32(dr["Room_Id"]);
                    person.Hostel_ID = Convert.ToInt32(dr["Hostel_ID"]);
                    person.Person_Name = dr["Person_Name"].ToString();
                    person.Person_Surname = dr["Person_Surname"].ToString();
                    person.Person_Mobile_Number = dr["Person_Mobile_Number"].ToString();
                    person.Person_Gender = dr["Person_Gender"].ToString();
                    person.Person_Address = dr["Person_Address"].ToString();
                    person.Person_Parent_Mobile_Number = dr["Person_Parent_Mobile_Number"].ToString();
                    person.Person_Email_ID = dr["Person_Email_ID"].ToString();
                    person.Person_City = dr["Person_City"].ToString();
                    person.Person_Profession = dr["Person_Profession"].ToString();
                    person.Person_WorkPlace = dr["Person_WorkPlace"].ToString();
                    person.Person_WorkPlace_MobileNumber = dr["Person_WorkPlace_MobileNumber"].ToString();
                    person.Person_JoningDate = DateOnly.FromDateTime(Convert.ToDateTime(dr["Person_JoningDate"]));
                    person.Person_PassWord = dr["Person_PassWord"].ToString();
                }
                ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);
                ViewBag.RoomList = personDal.GetRoomListByHostelId(_dbHelper,person.Hostel_ID);  // Load rooms for the selected hostel
                ViewBag.BedList = personDal.GetBedListByRoomID(_dbHelper,person.Room_ID);  // Load beds for the selected room
            }
            return View("AddEditPerson",person);
        }

        ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);

        ViewBag.RoomList = new List<Room_DropDownModel>();
        ViewBag.BedList = new List<Bed_DropDownmodel>();
        return View("AddEditPerson");
    }

    public IActionResult SavePerson(Person person)
    {
        PersonDal personDal = new PersonDal();
        if (person.Id == null)
        {
            personDal.InsertPerson(_dbHelper, person);
            TempData["Message"] = "Person Data Insert successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            personDal.UpdatePerson(_dbHelper, person);
            TempData["Message"] = "Person Data Update successfully!";
            TempData["AlertType"] = "success";
        }
        return RedirectToAction("AllPersonList");
    }

    public IActionResult personDelete(int Person_Id)
    {
        PersonDal person = new PersonDal();
        if (person.DeletePerson(_dbHelper, Person_Id))
        {
            TempData["Message"] = "Person Delete Successfully!";
            TempData["AlertType"] = "success";
        }
        return RedirectToAction("AllPersonList");
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
