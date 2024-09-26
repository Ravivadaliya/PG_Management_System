using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Room.Models;
using System.Data.SqlClient;
using System.Data;

namespace PG_Management_System.Areas.PG_Bed.Data;

public class BedDal
{
    public bool InsertRoomData(DatabaseHelper _dbHelper, Bed bed)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
       {
            new SqlParameter("Room_ID", SqlDbType.Int) { Value = bed.Room_ID },
           new SqlParameter("Bed_Number", SqlDbType.VarChar) { Value = bed.Bed_Number},
            new SqlParameter("Bed_Status", SqlDbType.Bit) { Value =false },
       };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Bed_Insert", sqlParameter);
        return (value == -1 ? false : true);
    }

    public DataTable GetAllBedFromHostelId(DatabaseHelper _dbHelper, int hostelid)
    {
        SqlParameter[] sqlParameters = new SqlParameter[] {
          new SqlParameter("Hostel_Id",SqlDbType.Int){Value= hostelid }
        };

        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectBedFromHostelId", sqlParameters);
        return dataTable;
    }

    public bool DeleteBed(DatabaseHelper _dbHelper, int bedid)
    {
        SqlParameter[] sqlParameters = new SqlParameter[] {
          new SqlParameter("Id",SqlDbType.Int){Value= bedid }
        };

        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Bed_Delete", sqlParameters);
        return (value == -1 ? false : true);
    }
}
