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
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() }
            };
            return _dbHelper.ExecuteStoredProcedure("SP_PG_Hostel_SelectByOwnerId", sqlParameter);
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

    public DataTable GetPgById(DatabaseHelper _dbHelper, int? hostelId, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = hostelId }
            };
            return _dbHelper.ExecuteStoredProcedure("SP_PG_Hostel_SelectById", sqlParameter);
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

    public bool InsertHostelData(Hostel hostel, DatabaseHelper _dbHelper)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() },
                new SqlParameter("@PG_Number", SqlDbType.VarChar) { Value = hostel.PG_Number },
                new SqlParameter("@Hostel_Address", SqlDbType.VarChar) { Value = hostel.Hostel_Address },
                new SqlParameter("@Hostel_MinimumPayment", SqlDbType.VarChar) { Value = hostel.Hostel_MinimumPayment },
                new SqlParameter("@Hostel_Type", SqlDbType.VarChar) { Value = hostel.Hostel_Type },
                new SqlParameter("@Hostel_Property_Category", SqlDbType.VarChar) { Value = hostel.Hostel_Property_Category },
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Insert", sqlParameter);
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

    public bool DeletePG(DatabaseHelper _dbHelper, int pgId)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = pgId }
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Delete", sqlParameter);
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

    public bool UpdatePgData(DatabaseHelper _dbHelper, Hostel hostel)
    {

        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = hostel.Id },
                new SqlParameter("Owner_ID", SqlDbType.Int) { Value = hostel.Owner_ID },
                new SqlParameter("PG_Number", SqlDbType.VarChar) { Value = hostel.PG_Number },
                new SqlParameter("Hostel_Address", SqlDbType.VarChar) { Value = hostel.Hostel_Address },
                 new SqlParameter("@Hostel_MinimumPayment", SqlDbType.VarChar) { Value = hostel.Hostel_MinimumPayment },
                new SqlParameter("@Hostel_Type", SqlDbType.VarChar) { Value = hostel.Hostel_Type },
                new SqlParameter("@Hostel_Property_Category", SqlDbType.VarChar) { Value = hostel.Hostel_Property_Category },
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Hostel_Update", sqlParameter);
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
}
