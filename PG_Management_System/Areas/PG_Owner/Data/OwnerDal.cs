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
        DataSet ds = _dbHelper.ExecuteStoredProcedureReturnDataSet("SP_SelectPGDetalsForDashBoard", sqlParameter);
        DashBoardDetail dashBoardDetail = new DashBoardDetail();

        // Check if the DataSet has tables and rows
        if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        {
            DataRow drHostelRoomBed = ds.Tables[0].Rows[0]; // First result set for hostels, rooms, and beds
            dashBoardDetail.TotalHostel = Convert.ToInt32(drHostelRoomBed["TotalHostel"]);
            dashBoardDetail.TotalRoom = Convert.ToInt32(drHostelRoomBed["TotalRoom"]);
            dashBoardDetail.TotalBed = Convert.ToInt32(drHostelRoomBed["TotalBed"]);
        }

        if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
        {
            DataRow drPerson = ds.Tables[1].Rows[0]; // Second result set for persons
            dashBoardDetail.TotalPerson = Convert.ToInt32(drPerson["TotalPerson"]);
        }

        return dashBoardDetail;
    }
}
