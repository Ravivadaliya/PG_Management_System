using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.BAL;
using System.Data;
namespace PG_Management_System.Areas.PG_Hostel.Controllers;

[CheckAccess]
[Area("PG_Hostel")]
[Route("PG_Hostel/[controller]/[action]")]
public class PG_HostelController : Controller
{
    private IConfiguration configuration;
    private readonly DatabaseHelper _dbHelper;

    public PG_HostelController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }
    public IActionResult AllPgList()
    {
        HostelDal hosteldal = new HostelDal();
        DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);
        return View("AllPgList", dataTable);
    }

    public IActionResult AddEditPG_Hostel()
    {

        return View();
    }

    public IActionResult Add(int? Id)
    {
        if (Id != null)
        {
            HostelDal hostelDal = new HostelDal();
            DataTable dataTable = hostelDal.GetPgById(_dbHelper, Id);

            Hostel hostel = new Hostel();
            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dr in dataTable.Rows)
                {
                    hostel.Id = Convert.ToInt32(dr["Id"]);
                    hostel.Owner_ID = Convert.ToInt32(dr["Owner_Id"]);
                    hostel.Hostel_Building_Number = dr["Hostel_Building_Number"].ToString();
                    hostel.Hostel_Address = dr["Hostel_Address"].ToString();
                }
            }
            return View("AddEditPG_Hostel", hostel);
        }
        return View("AddEditPG_Hostel");
    }

    public IActionResult DeletePG(int Id)
    {
        HostelDal hostelDal = new HostelDal();
        if (hostelDal.DeletePG(_dbHelper, Id))
        {
            TempData["Message"] = "PG Remove successfully!";
            TempData["AlertType"] = "success";
        }
        else
        {
            TempData["Message"] = "Error to Delete";
            TempData["AlertType"] = "error";
        }
        return RedirectToAction("AllPgList");
    }


    public IActionResult SavePg(Hostel hostel)
    {
        HostelDal hosteldal = new HostelDal();
        if (hostel.Id == null)
        {

            if (hosteldal.InserHostelData(hostel, _dbHelper))
            {
                TempData["Message"] = "PG added successfully!";
                TempData["AlertType"] = "success";  // 'success' for success alert
            }
            else
            {
                TempData["Message"] = "Failed to add PG.";
                TempData["AlertType"] = "error";  // 'error' for failure alert
            }
        }
        else
        {
            if (hosteldal.UpdatePgData(_dbHelper, hostel))
            {
                TempData["Message"] = "PG Data Update successfully!";
                TempData["AlertType"] = "success";  // 'success' for success alert
            }
            else
            {
                TempData["Message"] = "Failed To Update PG Data.";
                TempData["AlertType"] = "Error";
            }
            return RedirectToAction("AllPGList");
        }

        return RedirectToAction("AllPGList");
    }
}