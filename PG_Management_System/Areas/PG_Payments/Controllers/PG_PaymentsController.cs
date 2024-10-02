using Microsoft.AspNetCore.Mvc;
using PG_Management_System.BAL;
using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Payments.Data;
using System.Data;
using PG_Management_System.Areas.PG_Hostel.Data;
namespace PG_Management_System.Areas.PG_Payments.Controllers;

[CheckAccess]
[Area("PG_Payments")]
[Route("PG_Payments/{controller}/{action}")]
public class PG_PaymentsController(DatabaseHelper _dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = _dbHelper;

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

    public IActionResult PaymentsList(int Hostel_Id)
    {
        PaymentsDal paymentsDal = new PaymentsDal();
        DataTable dataTable = paymentsDal.GetPaymentListByOwnerId(_dbHelper,Hostel_Id);
        return View("PaymentsList", dataTable);
    }
}
