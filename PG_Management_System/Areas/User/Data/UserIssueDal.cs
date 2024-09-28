using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.User.Controllers;
using PG_Management_System.Areas.User.Models;

namespace PG_Management_System.Areas.User.Data;

public class UserIssueDal
{
    public DataTable GetAllissueByRoomId(DatabaseHelper _dbHelper,int room_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("@Room_Id",SqlDbType.Int){ Value= room_Id}
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectIssueListByRoomId", sqlParameter);
        return dataTable;
    }
    public bool InsertIssue(DatabaseHelper _dbHelper, UserIssue userIssue)
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
        return value==-1 ? false : true;
    }
    
}
