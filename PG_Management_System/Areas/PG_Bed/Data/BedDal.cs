using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Bed.Models;
using System.Data.SqlClient;
using System.Data;

public class BedDal
{
    public bool InsertRoomData(DatabaseHelper _dbHelper, Bed bed)
    {
        try
        {
            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("Room_ID", SqlDbType.Int) { Value = bed.Room_ID },
                new SqlParameter("Bed_Number", SqlDbType.VarChar) { Value = bed.Bed_Number }
            };

            object result = _dbHelper.ExecuteScalar("SP_CheckBedDuplicateEntry", checkParams);

            if (result != null && Convert.ToInt32(result) > 0)
            {
                return false;
            }

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Room_ID", SqlDbType.Int) { Value = bed.Room_ID },
                new SqlParameter("Bed_Number", SqlDbType.VarChar) { Value = bed.Bed_Number },
                new SqlParameter("Bed_Status", SqlDbType.Bit) { Value = false }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Bed_Insert", sqlParameter);
            return value != -1;
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

    public DataTable GetAllBedFromHostelId(DatabaseHelper _dbHelper, int hostelid)
    {
        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("Hostel_Id", SqlDbType.Int) { Value = hostelid }
            };

            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectBedFromHostelId", sqlParameters);
            return dataTable;
        }
        catch (SqlException ex)
        {
            return new DataTable();
        }
        catch (Exception ex)
        {
            return new DataTable();
        }
    }

    public bool DeleteBed(DatabaseHelper _dbHelper, int bedid)
    {
        try
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = bedid }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Bed_Delete", sqlParameters);
            return value != -1;
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
