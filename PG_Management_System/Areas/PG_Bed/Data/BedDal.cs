using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Bed.Models;
using System.Data.SqlClient;
using System.Data;

public class BedDal
{
    public bool InsertBedData(DatabaseHelper _dbHelper, Bed bed)
    {
        try
        {
            //SqlParameter[] checkParams = new SqlParameter[]
            //{
            //    new SqlParameter("Room_ID", SqlDbType.Int) { Value = bed.Room_ID },
            //    new SqlParameter("Bed_Number", SqlDbType.VarChar) { Value = bed.Bed_Number }
            //};

            //object result = _dbHelper.ExecuteScalar("SP_CheckBedDuplicateEntry", checkParams);

            //if (result != null && Convert.ToInt32(result) > 0)
            //{
            //    return false;
            //}

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

    public bool insertPersonOnBed(DatabaseHelper _dbHelper, int Bed_ID, int Person_ID, int Deposit, string Payment_Cycle, DateOnly Payment_Date)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter("Bed_ID",SqlDbType.Int){Value= Bed_ID},
            new SqlParameter("Person_ID",SqlDbType.Int){Value= Person_ID},
            new SqlParameter("Deposit",SqlDbType.Int){Value= Deposit},
            new SqlParameter("Payment_Cycle",SqlDbType.VarChar){Value= Payment_Cycle},
           new SqlParameter("@Payment_Date", Payment_Date.ToDateTime(TimeOnly.MinValue).Date) // Pass only the Date component
        };

        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_InsertPersonOnBed", sqlParameter);
        return value == -1 ? false : true;
    }

    public bool RemovePersonFromBed(DatabaseHelper _dbhelper, int Bed_Id)
    {
        try
        {

            SqlParameter[] sqlParameter = new SqlParameter[] {
            new SqlParameter ("Bed_Id",SqlDbType.Int){Value = Bed_Id  }
            };

            int value = _dbhelper.ExecuteStoredProcedureNonQuery("SP_RemovePersonFromBed", sqlParameter);
            return value == -1 ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}
