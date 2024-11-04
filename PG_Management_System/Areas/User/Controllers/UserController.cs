using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Payments.Data;
using PG_Management_System.Areas.PG_Payments.Models;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.User.Data;
using PG_Management_System.Areas.User.Models;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.User.Controllers
{
    [ClientCkeckAccess]
    [Area("User")]
    [Route("User")]
    public class UserController : Controller
    {
        private readonly DatabaseHelper _dbHelper;

        public UserController(DatabaseHelper dbHelper)
        {
            _dbHelper = dbHelper;
            _dbHelper.OpenConnection();
        }

        [HttpGet("IssueForm")]
        public IActionResult IssueForm()
        {
            return View();
        }


        //public IActionResult PaymentForm()
        //{
        //    return View();
        //}

        [HttpGet("UserPaymentList")]
        public IActionResult UserPaymentList()
        {
            PaymentsDal paymentsDal = new PaymentsDal();
            DataTable dataTable = paymentsDal.selectPaymentListFromPersonID(_dbHelper);
            return View("UserPaymentList",dataTable);
        }

        [HttpGet("UserDashBoard")]
        public IActionResult UserDashBoard()
        {
            try
            {
                PersonDal personDal = new PersonDal();
                DataTable dataTable = personDal.GetPersonAllDetailsById(_dbHelper, Convert.ToInt32(CV.Person_Id()));
                return View("UserDashBoard", dataTable);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("UserDashBoard");
            }
        }


        [HttpGet("UserIssueList")]
        public IActionResult UserIssueList(int Room_ID)
        {
            try
            {
                UserDal userIssueDal = new UserDal();
                DataTable dataTable = userIssueDal.GetAllissueByRoomId(_dbHelper, Room_ID, out string errorMessage);

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    TempData["Message"] = errorMessage;
                    TempData["AlertType"] = "error";
                    return RedirectToAction("UserDashBoard");
                }

                return View("UserIssueList", dataTable);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("UserDashBoard");
            }
        }

        [HttpPost("SaveIssue")]
        public IActionResult SaveIssue(Models.User userIssue)
        {
            try
            {
                UserDal userIssueDal = new UserDal();
                string imgpath = userIssue.Issue_ImgPath;
                string description = userIssue.Issue_Description;

                PersonDal personDal = new PersonDal();
                DataTable dataTable = personDal.GetPersonById(_dbHelper, Convert.ToInt32(CV.Person_Id()));

                Models.User userIssue1 = new Models.User();
                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dataTable.Rows)
                    {
                        userIssue1.Person_ID = Convert.ToInt32(CV.Person_Id());
                        userIssue1.Room_ID = Convert.ToInt32(dataRow["Room_ID"]);
                        userIssue1.Hostel_ID = Convert.ToInt32(dataRow["Hostel_ID"]);
                        userIssue1.Owner_ID = Convert.ToInt32(dataRow["Owner_ID"]);
                        userIssue1.Issue_ImgPath = imgpath;
                        userIssue1.Issue_Description = description;
                        userIssue1.Issue_Status = false;
                    }
                }
                else
                {
                    TempData["Message"] = "Some problem occurred during reporting your issue.";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("UserDashBoard");
                }

                if (userIssueDal.InsertIssue(_dbHelper, userIssue1))
                {
                    TempData["Message"] = "Your issue has been submitted.";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Error while Your issue submiting";
                    TempData["AlertType"] = "error";
                }

                return RedirectToAction("UserDashBoard");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("UserDashBoard");
            }
        }

        [HttpGet("AnnouncementList")]
        public IActionResult AnnouncementList()
        {
            try
            {
                int Person_Id = Convert.ToInt32(CV.Person_Id());
                UserDal userIssueDal = new UserDal();
                DataTable datatable = userIssueDal.GetUserNewNotification(_dbHelper, Person_Id);

                if (userIssueDal.UpdateNotificationStatus(_dbHelper))
                {
                    return View("NewAnnouncementList", datatable);
                }

                return View("NewAnnouncementList", datatable);
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("UserDashBoard");
            }
        }

        [HttpGet("GetNewNotificationCountByPersonId")]
        public int GetNewNotificationCountByPersonId()
        {
            try
            {
                int issuesCount = 0;
                SqlParameter[] sqlParameters = new SqlParameter[]
                {
                new SqlParameter("Person_Id", SqlDbType.Int) { Value = CV.Person_Id() }
                };

                object result = _dbHelper.ExecuteScalar("SP_SelectuserNewNotificationCountByPersonId", sqlParameters);

                if (result != null && Convert.ToInt32(result) > 0)
                {
                    return Convert.ToInt32(result);
                }

                return issuesCount;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        [HttpPost("SavePayment")]
        public IActionResult SavePayment(Payments payments)
        {
            try
            {

                if (payments.File != null)
                {
                    UserDal userDal = new UserDal();
                    if (userDal.InsertPayments(_dbHelper, payments))
                    {
                        TempData["Message"] = "Your payment details has been submitted.";
                        TempData["AlertType"] = "success";
                    }
                    else
                    {
                        TempData["Message"] = "Error while Your payment details submiting";
                        TempData["AlertType"] = "error";
                    }
                }
                else
                {
                    TempData["Message"] = "Image not found try agian!";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("PaymentForm");
                }

                return RedirectToAction("UserDashBoard");
            }
            catch (Exception ex)
            {
                TempData["Message"] = "An unexpected error occurred";
                TempData["AlertType"] = "error";
                return RedirectToAction("UserDashBoard");
            }
        }
    }
}
