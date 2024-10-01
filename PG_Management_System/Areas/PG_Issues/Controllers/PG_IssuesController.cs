using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Issues.Data;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Issues.Controllers;

[CheckAccess]
[Area("PG_Issues")]
[Route("PG_Issues/{controller}/{action}")]
public class PG_IssuesController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_IssuesController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    public IActionResult AllIssuesList()
    {
        IssueDal issueDal = new IssueDal();
        DataTable dataTable = issueDal.GetAllissueByOwnerId(_dbHelper);
        return View("AllIssuesList", dataTable);
    }

    public IActionResult UpdateStatus(int Issue_Id)
    {
        try
        {
            IssueDal issueDal = new IssueDal();

            if (issueDal.UpdateIssueStatus(_dbHelper, Issue_Id))
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

    public IActionResult GetPendingIssuesCount()
    {
        IssueDal issueDal = new IssueDal();
        IActionResult result = issueDal.GetPendingIssueCount(_dbHelper);
        return result;

    }


    public int GetIssuesCountByOwnerId()
    {
        int issuesCount = 0;
        SqlParameter[] sqlParameters = new SqlParameter[] {
            new SqlParameter("Owner_Id",SqlDbType.Int){Value=CV.Owner_Id()}
        };
        object result = (int)_dbHelper.ExecuteScalar("SP_Issue_PenddingCount", sqlParameters);

        if (result != null && Convert.ToInt32(result) > 0)
        {
            return Convert.ToInt32(result);
        }
        else
        {
            return issuesCount;
        }
    }
}
