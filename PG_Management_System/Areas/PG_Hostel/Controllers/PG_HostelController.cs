using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.BAL;
using PG_Management_System.Helper;
using System.Data;

namespace PG_Management_System.Areas.PG_Hostel.Controllers
{
    [CheckAccess]
    [Area("PG_Hostel")]
    [Route("PG")]
    public class PG_HostelController : Controller
    {
        private readonly DatabaseHelper _dbHelper;
        private readonly AESEncryptionHelper _aesencryptionHelper;
        public PG_HostelController(DatabaseHelper dbHelper, AESEncryptionHelper aesencryptionHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
            _aesencryptionHelper = aesencryptionHelper;
        }

        [HttpGet("PgList")]
        public IActionResult AllPgList()
        {
            HostelDal hostelDal = new HostelDal();
            DataTable dataTable = hostelDal.GetAllPGByOwnerId(_dbHelper);

            return View("AllPgList", dataTable);
        }



        //public IActionResult AddEditPG_Hostel()
        //{
        //    return View();
        //}

        [HttpGet("Add")]
        public IActionResult Add(int? id)
        {
            try
            {
                if (id != null)
                {
                    //// Decrypt the encrypted Person_Id
                    //var decryptedPersonId = _aesencryptionHelper.Decrypt(id);

                    //int? PGID = Convert.ToInt32(decryptedPersonId);


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
                        hostel.PG_Number = dr["PG_Number"].ToString();
                        hostel.Hostel_Address = dr["Hostel_Address"].ToString();
                        hostel.Hostel_MinimumPayment = dr["Hostel_MinimumPayment"].ToString();
                        hostel.Hostel_Type = dr["Hostel_Type"].ToString();
                        hostel.Hostel_Property_Category = dr["Hostel_Property_Category"].ToString();
                        hostel.Hostel_Floor = dr["Hostel_Floor"].ToString();
                        hostel.Hostel_Society = dr["Hostel_Society"].ToString();
                        hostel.Hostel_Gender = dr["Hostel_Gender"].ToString();
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

        [HttpPost("DeletePG")]
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

        [HttpPost("SavePg")]
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
