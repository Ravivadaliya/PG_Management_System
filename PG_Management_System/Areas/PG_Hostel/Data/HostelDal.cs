using DatabaseHelperLibrary;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.BAL;
namespace PG_Management_System.Areas.PG_Hostel.Data;

public class HostelDal
{

    public DataTable GetAllPGByOwnerId(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Owner_ID",SqlDbType.Int){ Value= CV.Owner_Id() }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Hostel_SelectByOwnerId", sqlParameter);
        return dataTable;
    }
    public DataTable GetPgById(DatabaseHelper _dbHelper, int? HostelId)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Id",SqlDbType.Int){ Value= HostelId }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Hostel_SelectById", sqlParameter);
        return dataTable;
    }
    
    public bool InserHostelData(Hostel hostel, DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() },
            new SqlParameter("@Hostel_Building_Number", SqlDbType.VarChar) { Value = hostel.Hostel_Building_Number},
            new SqlParameter("@Hostel_Address", SqlDbType.VarChar) { Value = hostel.Hostel_Address },
        };

        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Insert", sqlParameter);
        return (value == -1 ? false : true);
    }

    public bool DeletePG(DatabaseHelper _dbHelper, int pgid)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Id",SqlDbType.Int){ Value=pgid }
        };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Delete", sqlParameter);
        return (value == -1 ? false : true);
    }
   
    public bool UpdatePgData(DatabaseHelper _dbHelper, Hostel hostel)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
         {
            new SqlParameter ("Id",SqlDbType.Int){Value= hostel.Id},
            new SqlParameter ("Owner_ID",SqlDbType.Int){Value= hostel.Owner_ID},
            new SqlParameter ("Hostel_Building_Number",SqlDbType.VarChar){Value= hostel.Hostel_Building_Number},
            new SqlParameter ("Hostel_Address",SqlDbType.VarChar){Value= hostel.Hostel_Address}
         };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Update", sqlParameter);
        return (value == -1 ? false : true);
    }
}
