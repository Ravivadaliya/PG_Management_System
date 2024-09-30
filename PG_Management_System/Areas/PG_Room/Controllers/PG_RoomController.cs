using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Room.Data;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Room.Controllers
{
    [CheckAccess]
    [Area("PG_Room")]
    [Route("Room")]
    public class PG_RoomController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public PG_RoomController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
        }

        [HttpGet("PGList")]
        public IActionResult PGList()
        {

            HostelDal hosteldal = new HostelDal();
            DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);
            return View("PGList", dataTable);

        }

        [HttpGet("RoomList")]
        public IActionResult AllRoomListByHostelId(string Id)
        {
            try
            {
                RoomDal roomDal = new RoomDal();
                DataTable dataTable = roomDal.GetAllRoomByHostelId(_dbHelper, Id);
                return View("AllRoomList", dataTable);
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
                TempData["AlertType"] = "error";
                return RedirectToAction("ErrorPage");
            }
        }

        [HttpGet("AddRoom")]
        public IActionResult Add(int? Id, int Hostel_Id)
        {
            try
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
                            room.Room_GenderAllowed = dr["Room_GenderAllowed"].ToString();
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
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
                TempData["AlertType"] = "error";
                return RedirectToAction("ErrorPage");
            }
        }

        [HttpPost("SaveStaff")]
        public IActionResult SaveRoom(Room room)
        {
            string errorMessage = string.Empty; // Initialize error message

            try
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
                        TempData["Message"] = $"Failed to add Room";
                        TempData["AlertType"] = "error";
                    }
                }
                else
                {
                    if (roomDal.UpdateRoomData(_dbHelper, room, ref errorMessage))
                    {
                        TempData["Message"] = "Room data updated successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = $"Failed to update Room data";
                        TempData["AlertType"] = "error";
                    }
                }

                return RedirectToAction("PGList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("PGList");
            }
        }

        [HttpPost("DeleteRoom")]
        public IActionResult DeleteRoom(int Id)
        {
            try
            {
                RoomDal roomDal = new RoomDal();
                string errorMessage = string.Empty; // Initialize error message
                if (roomDal.Deleteroom(_dbHelper, Id, ref errorMessage))
                {
                    TempData["Message"] = "Room deleted successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = $"Error deleting Room. {errorMessage}";
                    TempData["AlertType"] = "error";
                }
                return RedirectToAction("PGList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
                TempData["AlertType"] = "error";
                return RedirectToAction("PGList");
            }
        }
    }
}
