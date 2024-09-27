using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;

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
}
