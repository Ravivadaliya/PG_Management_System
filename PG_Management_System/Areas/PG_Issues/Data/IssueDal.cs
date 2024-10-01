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
        
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() }
            };
            return _dbHelper.ExecuteStoredProcedure("SP_SelectIssueListByownerId", sqlParameter);
        }
        catch (SqlException ex)
        {
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public bool UpdateIssueStatus(DatabaseHelper _dbHelper, int issueId)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@IssueID", SqlDbType.Int) { Value = issueId }
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_UpdateIssueStatus", sqlParameter);
            if (value == -1)
            {
                return false;
            }

            return true;
        }
        catch (SqlException ex)
        {
            return false;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public IActionResult GetPendingIssueCount(DatabaseHelper _dbHelper)
    {
        try
        {
            object result = _dbHelper.ExecuteScalar("SP_Issue_PendingCount", null);

            int pendingIssues = 0;

            if (result != null && int.TryParse(result.ToString(), out int issueCount))
            {
                pendingIssues = issueCount;
            }

            return new JsonResult(new { PendingIssues = pendingIssues });
        }
        catch (Exception ex)
        {
            return new JsonResult(new { error = "error" });
        }
    }
}
