using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Staff.Data;
using PG_Management_System.Areas.PG_Staff.Models;
using PG_Management_System.BAL;
using PG_Management_System.Helper;
using System.Data;

namespace PG_Management_System.Areas.PG_Staff.Controllers
{
    [CheckAccess]
    [Area("PG_Staff")]
    [Route("Staff")]
    public class PG_StaffController : Controller
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AESEncryptionHelper _aesencryptionHelper;
        public PG_StaffController(DatabaseHelper dbHelper,AESEncryptionHelper aESEncryptionHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
            _aesencryptionHelper = aESEncryptionHelper;
        }


        //public IActionResult AddEditPG_Staff()
        //{
        //    return View();
        //}

        [HttpGet("StaffList")]
        public IActionResult AllStaffList()
        {
                StaffDal staffDal = new StaffDal();
                DataTable dataTable = staffDal.GetAllStaffByOwnerId(_dbHelper);
                return View("AllStaffList", dataTable);
        }


        [HttpGet("AddStaff")]
        public IActionResult Add(int? Id)
        {
            try
            {
                if (Id != null)
                {
                    //var decryptedPersonId = _aesencryptionHelper.Decrypt(Id);

                    //int? StaffID = Convert.ToInt32(decryptedPersonId);
                    StaffDal staffDal = new StaffDal();
                    DataTable dataTable = staffDal.GetStaffById(_dbHelper, Id);
                    Staff staff = new Staff();

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dataTable.Rows)
                        {
                            staff.Id = Id;
                            staff.Owner_ID = Convert.ToInt32(dr["Owner_Id"]);
                            staff.Staff_Name = dr["Staff_Name"].ToString();
                            staff.Staff_Mobile_Number = dr["Staff_Mobile_Number"].ToString();
                            staff.Staff_Surname = dr["Staff_Surname"].ToString();
                            staff.Staff_Gender = dr["Staff_Gender"].ToString();
                            staff.Staff_Address = dr["Staff_Address"].ToString();
                            staff.Staff_City = dr["Staff_City"].ToString();
                        }
                    }
                    return View("AddEditPG_Staff", staff);
                }
                return View("AddEditPG_Staff");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllStaffList");
            }
        }


        [HttpPost("DeleteStaff")]
        public IActionResult DeleteStaff(int Id)
        {
            try
            {
                StaffDal staffDal = new StaffDal();
                string errorMessage = string.Empty;

                if (staffDal.DeleteStaff(_dbHelper, Id))
                {
                    TempData["Message"] = "Staff removed successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Error while detleting staff";
                    TempData["AlertType"] = "error";
                }
                return RedirectToAction("AllStaffList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllStaffList");
            }
        }


        [HttpPost("SaveStaff")]
        public IActionResult SaveStaff(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                // Log model state errors for debugging
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                // You can log errors to a logger or console
                foreach (var error in errors)
                {
                    Console.WriteLine(error); // For debugging
                }

                // Return the view with the current staff data
                return View("AddEditPG_Staff", staff);
            }
            try
            {
                StaffDal staffDal = new StaffDal();
                string errorMessage = string.Empty;

                if (staff.Id == null)
                {
                    if (staffDal.InsertStaffData(staff, _dbHelper))
                    {
                        TempData["Message"] = "Staff added successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Error while inserting staff data"; 
                        TempData["AlertType"] = "error";
                    }
                }
                else
                {
                    if (staffDal.UpdateStaffData(_dbHelper, staff, ref errorMessage))
                    {
                        TempData["Message"] = "Staff data updated successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Error occure while updating staff"; 
                        TempData["AlertType"] = "error";
                    }
                }
                return RedirectToAction("AllStaffList");
            }
            catch (Exception ex)
            {
                TempData["Message"] = $"An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllStaffList");
            }
        }
    }
}
