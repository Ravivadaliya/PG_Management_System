using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;

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
}
