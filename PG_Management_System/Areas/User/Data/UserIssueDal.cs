using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.User.Controllers;
using PG_Management_System.Areas.User.Models;

namespace PG_Management_System.Areas.User.Data;

public class UserIssueDal
{
    public DataTable GetAllissueByRoomId(DatabaseHelper _dbHelper, int room_Id, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                  new SqlParameter("@Room_Id", SqlDbType.Int) { Value = room_Id }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectIssueListByRoomId", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error fetching issues: {ex.Message}";
            return new DataTable(); // Return an empty DataTable in case of an error
        }
    }

    public bool InsertIssue(DatabaseHelper _dbHelper, UserIssue userIssue, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                    new SqlParameter("@Person_ID", SqlDbType.Int) { Value = userIssue.Person_ID },
                    new SqlParameter("@Room_ID", SqlDbType.Int) { Value = userIssue.Room_ID },
                    new SqlParameter("@Hostel_ID", SqlDbType.Int) { Value = userIssue.Hostel_ID },
                    new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = userIssue.Owner_ID },
                    new SqlParameter("@Issue_ImgPath", SqlDbType.VarChar) { Value = userIssue.Issue_ImgPath },
                    new SqlParameter("@Issue_Status", SqlDbType.Bit) { Value = userIssue.Issue_Status },
                    new SqlParameter("@Issue_Description", SqlDbType.VarChar) { Value = userIssue.Issue_Description }
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Isssue_Insert", sqlParameter);
            return value != -1;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error inserting issue: {ex.Message}";
            return false;
        }
    }

    public DataTable GetUserOldNotification(DatabaseHelper _dbHelper, int Person_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Person_Id",SqlDbType.Int){ Value= Person_Id}
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectuserOldNotificationbyPersonId", sqlParameter);
        return dataTable;
    }
    public DataTable GetUserNewNotification(DatabaseHelper _dbHelper, int Person_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("@Person_Id",SqlDbType.Int){ Value= Person_Id}
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectuserNotificationbyPersonId", sqlParameter);
        return dataTable;
    }
    public bool InsertIssue(DatabaseHelper _dbHelper, UserIssue userIssue)
    {
        try
        {

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
            new SqlParameter ("@Person_ID",SqlDbType.Int){ Value= userIssue.Person_ID},
            new SqlParameter ("@Room_ID",SqlDbType.Int){ Value= userIssue.Room_ID},
            new SqlParameter ("@Hostel_ID",SqlDbType.Int){ Value= userIssue.Hostel_ID},
            new SqlParameter ("@Owner_ID",SqlDbType.Int){ Value= userIssue.Owner_ID},
            new SqlParameter ("@Issue_ImgPath",SqlDbType.VarChar){ Value= userIssue.Issue_ImgPath},
            new SqlParameter ("@Issue_Status",SqlDbType.Bit){ Value= userIssue.Issue_Status},
            new SqlParameter ("@Issue_Description",SqlDbType.VarChar){ Value= userIssue.Issue_Description}
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Isssue_Insert", sqlParameter);
            return value == -1 ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UpdateNotificationStatus(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Person_Id",SqlDbType.Int){ Value= CV.Person_Id()},
        };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_UpdateUsernotificationStatus", sqlParameter);
        return value == -1 ? false : true;
    }
}
