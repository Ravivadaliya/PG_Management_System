using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Payments.Data;
using System.Data;
using PG_Management_System.Areas.PG_Hostel.Data;
namespace PG_Management_System.Areas.PG_Payments.Controllers;

[CheckAccess]
[Area("PG_Payments")]
[Route("Payments")]
public class PG_PaymentsController(DatabaseHelper _dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = _dbHelper;

    [HttpGet("SelectPG")]
    public IActionResult PGList()
    {
        try
        {
            HostelDal hosteldal = new HostelDal();
            DataTable dataTable = hosteldal.GetAllPGByOwnerId(_dbHelper);
            return View("PGList", dataTable);
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error loading PG list. Please try again.";
            TempData["AlertType"] = "error";
            return RedirectToAction("Index");
        }
    }

    [HttpGet("PenddingPayments")]
    public IActionResult penddingfPaymentsList(int Hostel_Id)
    {
        PaymentsDal paymentsDal = new PaymentsDal();
        DataTable dataTable = paymentsDal.GetPenddingPaymentListByOwnerIdAndHostelId(_dbHelper, Hostel_Id);
        return View("PaymentsList", dataTable);
    }

    [HttpGet("CompletePayments")]
    public IActionResult completePaymentsList(int Hostel_Id)
    {
        PaymentsDal paymentsDal = new PaymentsDal();
        DataTable dataTable = paymentsDal.GetCompletePaymentListByOwnerId(_dbHelper, Hostel_Id);
        return View("PaymentsList", dataTable);
    }

    [HttpPost("UpdatePaymentCompleteStatus")]
    public IActionResult UpdatePaymentCompleteStatus(int Payment_Id)
    {
        try
        {
            PaymentsDal paymentsDal = new PaymentsDal();
            if (paymentsDal.UpdatePaymentStatus(_dbHelper, Payment_Id))
            {
                return Json(new { success = true, message = "Payment Confirmed." });
            }
            else
            {
                return Json(new { success = false, message = "Error to payment confirmed!" });
            }
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error Update Status, Please try again.";
            TempData["AlertType"] = "error";
            return Json(new { success = false, message = "Error to payment confirmed!" });
        }
    }




}
