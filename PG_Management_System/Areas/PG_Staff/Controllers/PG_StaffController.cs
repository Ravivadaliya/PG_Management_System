using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32.SafeHandles;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Staff.Data;
using PG_Management_System.Areas.PG_Staff.Models;
using PG_Management_System.BAL;
using System.Data;
namespace PG_Management_System.Areas.PG_Staff.Controllers;


[CheckAccess]
[Area("PG_Staff")]
[Route("PG_Staff/[controller]/[action]")]
public class PG_StaffController : Controller
{
    private IConfiguration configuration;
    private readonly DatabaseHelper _dbHelper;

    public PG_StaffController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    public IActionResult AddEditPG_Staff()
    {
        return View();
    }
    public IActionResult DashBoard()
    {
        return View();
    }

    public IActionResult AllStaffList()
    {
        
        StaffDal staffdal = new StaffDal();
        DataTable dataTable = staffdal.GetAllStaffByOwnerId(_dbHelper);
        return View("AllStaffList", dataTable);
    }
    public IActionResult StaffProfile()
    {
        return View();
    }


    public IActionResult Add(int? Id)
    {
        if (Id != null)
        {
            StaffDal staffDal = new StaffDal();
            DataTable dataTable = staffDal.GetStaffById(_dbHelper, Id);

            Staff staff = new Staff();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
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

    public IActionResult DeleteStaff(int Id)
    {
        StaffDal staffDal = new StaffDal();
        if (staffDal.Deletestaff(_dbHelper, Id))
        {
            TempData["Message"] = "Staff Remove successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            TempData["Message"] = "Error to Delete";
            TempData["AlertType"] = "error";
        }
        return RedirectToAction("AllStaffList");
    }
    public IActionResult SaveStaff(Staff staff)
    {
        StaffDal staffdal = new StaffDal();
        if (staff.Id == null)
        {

            if (staffdal.InserStaffData(staff, _dbHelper))
            {
                TempData["Message"] = "Staff added successfully!";
                TempData["AlertType"] = "success";  // 'success' for success alert
            }
            else
            {
                TempData["Message"] = "Failed to add Staff.";
                TempData["AlertType"] = "error";  // 'error' for failure alert
            }
        }
        else
        {
            if (staffdal.UpdatePgData(_dbHelper, staff))
            {
                TempData["Message"] = "Staff Data Update successfully!";
                TempData["AlertType"] = "success";  // 'success' for success alert
            }
            else
            {
                TempData["Message"] = "Failed To Update Staff Data.";
                TempData["AlertType"] = "Error";
            }
            return RedirectToAction("AllStaffList");
        }

        return RedirectToAction("AllStaffList");
    }
}
