using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Issues.Data;

public class IssueDal
{
    public DataTable GetAllissueByOwnerId(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id() }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectIssueListByownerId", sqlParameter);
        return dataTable;
    }

    public bool UpdateIssueStatus(DatabaseHelper _dbhelper, int Issue_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("@IssueID",SqlDbType.Int){ Value= Issue_Id }
        };
        int value = _dbhelper.ExecuteStoredProcedureNonQuery("SP_PG_UpdateIssueStatus", sqlParameter);
        return value == -1 ? false : true;
    }

    public IActionResult GetPenddingIssueCount(DatabaseHelper _dbhelper)
    {
        object result = _dbhelper.ExecuteScalar("SP_Issue_PenddingCount", null);

        int pendingIssues = 0;

        if (result != null && int.TryParse(result.ToString(), out int issueCount))
        {
            pendingIssues = issueCount;
        }
        return new JsonResult(new { PendingIssues = pendingIssues });
    }


}
