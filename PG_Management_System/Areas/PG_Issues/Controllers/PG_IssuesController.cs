using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Issues.Data;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Issues.Controllers;

[CheckAccess]
[Area("PG_Issues")]
[Route("PG_Issues/[controller]/[action]")]
public class PG_IssuesController(DatabaseHelper dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = dbHelper;

    public static IssueDal issuesDal = new IssueDal();
    public IActionResult AllIssuesList()
    {
        DataTable dataTable = issuesDal.GetAllissueByOwnerId(_dbHelper);
        return View("AllIssuesList", dataTable);
    }

    public IActionResult UpdateStatus(int Issue_Id)
    {

        if (issuesDal.UpdateIssueStatus(_dbHelper, Issue_Id))
        {
            TempData["Message"] = "Done";
            TempData["AlertType"] = "success";
            return RedirectToAction("AllIssuesList");
        }
        else
        {
            TempData["Message"] = "Not Update Something was wrong!!";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllIssuesList");

        }
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
