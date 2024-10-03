using PG_Management_System.Areas.PG_Owner.Models;
using System.Data.SqlClient;
using System.Data;
using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.BAL;

namespace PG_Management_System.Areas.PG_Owner.Data;

public class OwnerDal
{
    public bool InsertAdminData(Owner owner, DatabaseHelper _dbHelper)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Owner_Name", SqlDbType.VarChar) { Value = owner.Owner_Name },
                new SqlParameter("@Owner_Surname", SqlDbType.VarChar) { Value = owner.Owner_Surname },
                new SqlParameter("@Owner_Gender", SqlDbType.VarChar) { Value = owner.Owner_Gender },
                new SqlParameter("@Owner_EmailId", SqlDbType.VarChar) { Value = owner.Owner_EmailId },
                new SqlParameter("@Owner_Mobile_Number", SqlDbType.VarChar) { Value = owner.Owner_Mobile_Number },
                new SqlParameter("@Owner_City", SqlDbType.VarChar) { Value = owner.Owner_City },
                new SqlParameter("@Owner_State", SqlDbType.VarChar) { Value = owner.Owner_State },
                new SqlParameter("@Owner_Address", SqlDbType.VarChar) { Value = owner.Owner_Address },
                new SqlParameter("@Owner_IsActive", SqlDbType.Int) { Value = 0 },
                new SqlParameter("@Owner_PassWord", SqlDbType.VarChar) { Value = owner.Owner_PassWord }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Owner_Insert", sqlParameter);
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

    public DataTable SelectOwnerByEmailPass(DatabaseHelper _dbHelper, string ownerEmail, string ownerPassword)
    {
       
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@EmailId", SqlDbType.VarChar) { Value = ownerEmail },
                new SqlParameter("@PassWord", SqlDbType.VarChar) { Value = ownerPassword }
            };
            return _dbHelper.ExecuteStoredProcedure("SP_SelectOwnerByEmailandPassword", sqlParameter);
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

    public DashBoardDetail PGDetalsForDashBoard(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
        new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() }
        };

        // Execute the stored procedure and get the DataTable
        DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectPGDetalsForDashBoard", sqlParameter);
        DashBoardDetail dashBoardDetail = new DashBoardDetail();

        // Check if the DataTable has rows
        if (dt.Rows.Count > 0)
        {
            // Populate the dashBoardDetail object with the data from the DataTable
            DataRow dr = dt.Rows[0]; // Assuming you want the first row
            dashBoardDetail.TotalHostel = Convert.ToInt32(dr["TotalHostel"]);
            dashBoardDetail.TotalRoom = Convert.ToInt32(dr["TotalRoom"]);
            dashBoardDetail.TotalPerson = Convert.ToInt32(dr["TotalPerson"]);
            dashBoardDetail.TotalBed = Convert.ToInt32(dr["TotalBed"]);
        }
        return dashBoardDetail;
    }
}
