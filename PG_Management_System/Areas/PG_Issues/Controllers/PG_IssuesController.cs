using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Issues.Data;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Issues.Controllers;

[CheckAccess]
[Area("PG_Issues")]
[Route("Issues")]
public class PG_IssuesController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_IssuesController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    [HttpGet("AllIssuesList")]
    public IActionResult AllIssuesList()
    {
        IssueDal issueDal = new IssueDal();
        string errorMessage;
        DataTable dataTable = issueDal.GetAllissueByOwnerId(_dbHelper);
        return View("AllIssuesList", dataTable);
    }

    [HttpPost("UpdateStatus/{issueId}")]
    public IActionResult UpdateStatus(int issueId)
    {
        try
        {
            IssueDal issueDal = new IssueDal();
            string errorMessage;

            if (issueDal.UpdateIssueStatus(_dbHelper, issueId))
            {
                TempData["Message"] = "Issue status updated successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["Message"] = "Error while updating issues status";
                TempData["AlertType"] = "error";
            }

            return RedirectToAction("AllIssuesList");
        }
        catch (Exception ex)
        {
            TempData["Message"] = "An unexpected error occurred";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllIssuesList");
        }
    }

    [HttpGet("GetPendingIssuesCount")]
    public IActionResult GetPendingIssuesCount()
    {
        IssueDal issueDal = new IssueDal();
        IActionResult result = issueDal.GetPendingIssueCount(_dbHelper);
        return result;

    }
}
