using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.PG_Staff.Models;
using PG_Management_System.Areas.PG_Room.Models;

namespace PG_Management_System.Areas.PG_Room.Data
{
    public class RoomDal
    {
        public DataTable GetAllRoomByHostelId(DatabaseHelper _dbHelper, string Hostel_Id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("Hostel_Id",SqlDbType.Int){ Value= Hostel_Id}
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectRoomListByHostelId", sqlParameter);
            return dataTable;
        }

        public DataTable GetRoomById(DatabaseHelper _dbHelper, int? Id)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter ("Id",SqlDbType.Int){ Value= Id}
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Room_SelectById", sqlParameter);
            return dataTable;
        }

        public bool UpdateRoomData(DatabaseHelper _dbHelper, Room room, ref string errorMessage)
        {
            string baseRoomNumber = room.Room_Number.Split('-')[0];
            string newRoomNumber = baseRoomNumber + "-" + room.Room_SharingType + "-" + room.Room_Type;

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = room.Id },
                new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = room.Hostel_ID },
                new SqlParameter("Room_Number", SqlDbType.VarChar) { Value = newRoomNumber},
                new SqlParameter("Room_SharingType", SqlDbType.VarChar) { Value = room.Room_SharingType},
                new SqlParameter("Room_Type", SqlDbType.VarChar) { Value = room.Room_Type},
                new SqlParameter("Room_GenderAllowed", SqlDbType.VarChar) { Value = room.Room_GenderAllowed},
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Room_Update", sqlParameter);
            if (value == -1)
            {
                errorMessage = "Failed to update room data in the database.";
                return false;
            }
            return true;
        }

        public bool InsertRoomData(DatabaseHelper _dbHelper, Room room)
        {
            string checkroom = room.Room_Number + "-" + room.Room_SharingType + "-" + room.Room_Type;

            SqlParameter[] checkParams = new SqlParameter[]
            {
                new SqlParameter("Room_Number", SqlDbType.VarChar) { Value = checkroom },
                new SqlParameter("Hostel_ID", SqlDbType.VarChar) { Value = room.Hostel_ID },
            };

            object result = _dbHelper.ExecuteScalar("SP_CheckRoomDuplicateEntry", checkParams);

            // Check if the room already exists
            if (result != null && Convert.ToInt32(result) > 0)
            {
                return false;
            }

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = room.Hostel_ID },
                new SqlParameter("Room_Number", SqlDbType.VarChar) { Value = room.Room_Number + "-" + room.Room_SharingType + "-" + room.Room_Type },
                new SqlParameter("Room_SharingType", SqlDbType.VarChar) { Value = room.Room_SharingType },
                new SqlParameter("Room_AllowcateBed", SqlDbType.VarChar) { Value = 0 },
                new SqlParameter("Room_Createbed", SqlDbType.VarChar) { Value = 0 },
                new SqlParameter("Room_Type", SqlDbType.VarChar) { Value = room.Room_Type },
                new SqlParameter("Room_GenderAllowed", SqlDbType.VarChar) { Value = room.Room_GenderAllowed }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Room_Insert", sqlParameter);
            if (value == -1)
            {
                return false;
            }
            return true;
        }

        public bool Deleteroom(DatabaseHelper _dbHelper, int Id, ref string errorMessage)
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = Id }
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Room_Delete", sqlParameter);
            if (value == -1)
            {
                errorMessage = "Failed to delete room from the database.";
                return false;
            }
            return true;
        }
    }
}
