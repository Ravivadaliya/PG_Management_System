using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Hostel.Controllers
{
    [CheckAccess]
    [Area("PG_Hostel")]
    [Route("PG_Hostel/{controller}/{action}")]
    public class PG_HostelController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public PG_HostelController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
        }

        public IActionResult AllPgList()
        {
            HostelDal hostelDal = new HostelDal();
            DataTable dataTable = hostelDal.GetAllPGByOwnerId(_dbHelper);

            return View("AllPgList", dataTable);
        }


        public IActionResult AddEditPG_Hostel()
        {

            return View();

        }

        public IActionResult Add(int? id)
        {
            try
            {
                if (id != null)
                {
                    HostelDal hostelDal = new HostelDal();
                    string errorMessage;
                    DataTable dataTable = hostelDal.GetPgById(_dbHelper, id, out errorMessage);

                    if (dataTable == null)
                    {
                        TempData["Message"] = errorMessage;
                        TempData["AlertType"] = "error";
                        return RedirectToAction("AllPgList");
                    }

                    Hostel hostel = new Hostel();
                    if (dataTable.Rows.Count > 0)
                    {
                        DataRow dr = dataTable.Rows[0];
                        hostel.Id = Convert.ToInt32(dr["Id"]);
                        hostel.Owner_ID = Convert.ToInt32(dr["Owner_Id"]);
                        hostel.Hostel_Building_Number = dr["Hostel_Building_Number"].ToString();
                        hostel.Hostel_Address = dr["Hostel_Address"].ToString();
                    }

                    return View("AddEditPG_Hostel", hostel);
                }

                return View("AddEditPG_Hostel");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("AllPgList");
            }
        }

        public IActionResult DeletePG(int id)
        {
            try
            {
                HostelDal hostelDal = new HostelDal();

                if (hostelDal.DeletePG(_dbHelper, id))
                {
                    TempData["Message"] = "PG removed successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Make sure first delete all details which is related to this PG";
                    TempData["AlertType"] = "error";
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "error occurred while deleting PG";
                TempData["AlertType"] = "error";
            }

            return RedirectToAction("AllPgList");
        }

        public IActionResult SavePg(Hostel hostel)
        {
            try
            {
                HostelDal hostelDal = new HostelDal();

                if (hostel.Id == null)
                {
                    if (hostelDal.InsertHostelData(hostel, _dbHelper))
                    {
                        TempData["Message"] = "PG added successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Error while inserting data";
                        TempData["AlertType"] = "error";
                    }
                }
                else
                {
                    if (hostelDal.UpdatePgData(_dbHelper, hostel))
                    {
                        TempData["Message"] = "PG data updated successfully!";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Error while Updating data";
                        TempData["AlertType"] = "error";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Message"] = "Error occurred while save pg details";
                TempData["AlertType"] = "error";
            }

            return RedirectToAction("AllPgList");
        }
    }
}
