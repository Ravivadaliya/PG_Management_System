using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Person.Controllers
{
    [CheckAccess]
    [Area("PG_Person")]
    [Route("Person")]
    public class PG_PersonController : Controller
    {
        private readonly DatabaseHelper _dbHelper;
        public static string Person_Image = "";

        public PG_PersonController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
        }

        [HttpGet("AllPerson")]
        public IActionResult AllPersonList()
        {

            PersonDal personDal = new PersonDal();
            DataTable dataTable = personDal.GetAllPersonByOwnerId(_dbHelper);
            return View("AllPersonList", dataTable);

        }

        [HttpGet("AddPerson")]
        public IActionResult AddEditPerson()
        {

            return View();

        }

        [HttpGet("AddEditPerson/{Person_Id}")]
        public IActionResult Add(int? Person_Id)
        {
            try
            {
                PersonDal personDal = new PersonDal();
                Person person = new Person();

                if (Person_Id != null)
                {
                    DataTable dataTable = personDal.GetPersonById(_dbHelper, Person_Id);

                    if (dataTable == null)
                    {
                        TempData["Message"] = $"Person Not Found";
                        TempData["AlertType"] = "error";
                        return RedirectToAction("AllPersonList");
                    }

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
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
                            person.Person_Image = dr["Person_Image"].ToString();
                            person.Person_PassWord = dr["Person_PassWord"].ToString();

                            // Get the bed list based on the room ID
                            var bedList = personDal.GetBedListByRoomID(_dbHelper, person.Room_ID);
                            if (bedList.All(b => b.Id != person.Bed_ID))
                            {
                                var selectedBed = personDal.GetBedById(_dbHelper, person.Bed_ID);
                                if (selectedBed != null)
                                {
                                    bedList.Add(selectedBed);
                                }
                            }
                            ViewBag.BedList = bedList;

                            // Get the room list based on the hostel ID
                            var roomList = personDal.GetRoomListByHostelId(_dbHelper, person.Hostel_ID);
                            if (roomList.All(r => r.Id != person.Room_ID))
                            {
                                var selectedRoom = personDal.GetRoomById(_dbHelper, person.Room_ID);
                                if (selectedRoom != null)
                                {
                                    roomList.Add(selectedRoom);
                                }
                            }
                            ViewBag.RoomList = roomList;

                            // Get the hostel list
                            var hostelList = personDal.GetHostelListByOwnerID(_dbHelper);
                            if (hostelList.All(h => h.Id != person.Hostel_ID))
                            {
                                var selectedHostel = personDal.GetHostelById(_dbHelper, person.Hostel_ID);
                                if (selectedHostel != null)
                                {
                                    hostelList.Add(selectedHostel);
                                }
                            }
                            ViewBag.HostelList = hostelList;
                        }
                        Person_Image = person.Person_Image;
                        return View("AddEditPerson", person);
                    }
                }

                // Set default lists if no Person_Id is provided
                ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);
                ViewBag.RoomList = new List<Room_DropDownModel>();
                ViewBag.BedList = new List<Bed_DropDownmodel>();

                return View("AddEditPerson");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllPersonList");
            }
        }

        [HttpGet("PersonDetails/{Person_Id}")]
        public IActionResult PersonDetails(int Person_Id)
        {
            try
            {
                PersonDal personDal = new PersonDal();
                string errorMessage;
                DataTable dataTable = personDal.GetAllPersonByOwnerIdAndPersonId(_dbHelper, Person_Id, out errorMessage);

                if (dataTable == null)
                {
                    TempData["Message"] = $"Error occurred: {errorMessage}";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("AllP");
                }

                return View("PersonDetails", dataTable);
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllPersonList");
            }
        }

        [HttpPost("SavePerson")]
        public IActionResult SavePerson(Person person)
        {
            try
            {
                PersonDal personDal = new PersonDal();
                string errorMessage;

                if (person.Id == null)
                {
                    if (personDal.InsertPerson(_dbHelper, person, Person_Image, out errorMessage))
                    {
                        TempData["Message"] = "Person data inserted successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = $"Failed to insert person data: {errorMessage}";
                        TempData["AlertType"] = "error";
                    }
                }
                else
                {
                    if (personDal.UpdatePerson(_dbHelper, person))
                    {
                        TempData["Message"] = "Person data updated successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Failed to update person data";
                        TempData["AlertType"] = "error";
                    }
                }
                return RedirectToAction("AllPersonList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllPersonList");
            }
        }

        [HttpPost("personDelete")]
        public IActionResult personDelete(int Person_Id)
        {
            try
            {
                PersonDal personDal = new PersonDal();
                string errorMessage;
                if (personDal.DeletePerson(_dbHelper, Person_Id, out errorMessage))
                {
                    TempData["Message"] = "Person deleted successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Failed to delete person";
                    TempData["AlertType"] = "error";
                }
                return RedirectToAction("AllPersonList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllPersonList");
            }
        }

        #region PG_Room_RoomDropDownByHostelId
        public IActionResult PG_Room_RoomDropDownByHostelId(int Hostel_Id)
        {
            try
            {
                PersonDal personDal = new PersonDal();
                var rooms = personDal.GetRoomListByHostelId(_dbHelper, Hostel_Id);

                if (rooms == null)
                {
                    TempData["Message"] = $"Error occurred";
                    TempData["AlertType"] = "error";
                    return Json(new { success = false });
                }

                return Json(new { success = true, data = rooms });
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return Json(new { success = false, message = ex.Message });
            }
        }
        #endregion
    }
}
