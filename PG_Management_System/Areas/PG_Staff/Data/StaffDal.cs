using DatabaseHelperLibrary;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Staff.Models;

namespace PG_Management_System.Areas.PG_Staff.Data;

public class StaffDal
{
    public DataTable GetStaffById(DatabaseHelper _dbHelper, int? StaffId)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Id",SqlDbType.Int){ Value= StaffId }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Staff_SelectById", sqlParameter);
        return dataTable;
    }
    public DataTable GetAllStaffByOwnerId(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Owner_ID",SqlDbType.Int){ Value= CV.Owner_Id() }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Staff_SelectByOwnerId", sqlParameter);
        return dataTable;
    }


    public bool InserStaffData(Staff staff, DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() },
            new SqlParameter("@Staff_Name", SqlDbType.VarChar) { Value = staff.Staff_Name},
            new SqlParameter("@Staff_Surname", SqlDbType.VarChar) { Value = staff.Staff_Surname},
            new SqlParameter("@Staff_Mobile_Number", SqlDbType.VarChar) { Value = staff.Staff_Mobile_Number},
            new SqlParameter("@Staff_Address", SqlDbType.VarChar) { Value = staff.Staff_Address},
            new SqlParameter("@Staff_Gender", SqlDbType.VarChar) { Value = staff.Staff_Gender},
            new SqlParameter("@Staff_City", SqlDbType.VarChar) { Value = staff.Staff_City},
        };

        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Insert", sqlParameter);
        return (value == -1 ? false : true);
    }

    public bool Deletestaff(DatabaseHelper _dbHelper, int staffid)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Id",SqlDbType.Int){ Value=staffid }
        };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Delete", sqlParameter);
        return (value == -1 ? false : true);
    }


    public bool UpdatePgData(DatabaseHelper _dbHelper, Staff staff)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
       {
            new SqlParameter("@Id", SqlDbType.Int) { Value = staff.Id },
            new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = staff.Owner_ID },
            new SqlParameter("@Staff_Name", SqlDbType.VarChar) { Value = staff.Staff_Name},
            new SqlParameter("@Staff_Surname", SqlDbType.VarChar) { Value = staff.Staff_Surname},
            new SqlParameter("@Staff_Mobile_Number", SqlDbType.VarChar) { Value = staff.Staff_Mobile_Number},
            new SqlParameter("@Staff_Address", SqlDbType.VarChar) { Value = staff.Staff_Address},
            new SqlParameter("@Staff_Gender", SqlDbType.VarChar) { Value = staff.Staff_Gender},
            new SqlParameter("@Staff_City", SqlDbType.VarChar) { Value = staff.Staff_City},
       };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Update", sqlParameter);
        return (value == -1 ? false : true);
    }

}
