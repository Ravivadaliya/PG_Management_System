using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.Areas.PG_Hostel.Data;
using PG_Management_System.Areas.PG_Bed.Models;

namespace PG_Management_System.Areas.PG_Person.Data;

public class PersonDal
{
    public DataTable GetAllPersonByOwnerId(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id() }
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Person_SelectAllByOwnerId", sqlParameter);
        return dataTable;
    }

    //public bool InsertPerson(DatabaseHelper _dbHelper, Person person)
    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("Bed_ID",SqlDbType.Int){ Value= person.Bed_ID },
    //        new SqlParameter ("Room_ID",SqlDbType.Int){ Value= person.Room_ID },
    //        new SqlParameter ("Hostel_ID",SqlDbType.Int){ Value= person.Hostel_ID },
    //        new SqlParameter ("Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id },
    //        new SqlParameter ("Person_Name",SqlDbType.VarChar){ Value= person.Person_Name },
    //        new SqlParameter ("Person_Surname",SqlDbType.VarChar){ Value= person.Person_Surname },
    //        new SqlParameter ("Person_Mobile_Number",SqlDbType.VarChar){ Value= person.Person_Mobile_Number },
    //        new SqlParameter ("Person_Parent_Mobile_Number",SqlDbType.VarChar){ Value= person.Person_Parent_Mobile_Number },
    //        new SqlParameter ("Person_Email_ID",SqlDbType.VarChar){ Value= person.Person_Email_ID },
    //        new SqlParameter ("Person_Gender",SqlDbType.VarChar){ Value= person.Person_Gender },
    //        new SqlParameter ("Person_Address",SqlDbType.VarChar){ Value= person.Person_Address },
    //        new SqlParameter ("Person_City",SqlDbType.VarChar){ Value= person.Person_City },
    //        new SqlParameter ("Person_Profession",SqlDbType.VarChar){ Value= person.Person_Profession },
    //        new SqlParameter ("Person_WorkPlace",SqlDbType.VarChar){ Value= person.Person_WorkPlace },
    //        new SqlParameter ("Person_WorkPlace_MobileNumber",SqlDbType.VarChar){ Value= person.Person_WorkPlace_MobileNumber },
    //        new SqlParameter ("Person_JoningDate",SqlDbType.Date){ Value= person.Person_JoningDate }

    //    };
    ////}
    ///
    public List<Hostel_DropDownModel> GetHostelListByOwnerID(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
           {
            new SqlParameter ("@Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id() }
           };
        DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectHostelsByOwnerId", sqlParameter);
        List<Hostel_DropDownModel> hostel_DropDownModels = new List<Hostel_DropDownModel>();
        foreach (DataRow dr in dt.Rows)
        {
            Hostel_DropDownModel model = new Hostel_DropDownModel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Hostel_Building_Number = dr["Hostel_Building_Number"].ToString()
            };

            // Add the mapped model to the list
            hostel_DropDownModels.Add(model);
        }

        return hostel_DropDownModels;
    }

    public List<Room_DropDownModel> GetRoomListByHostelId(DatabaseHelper _dbHelper, int Hostel_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
          {
            new SqlParameter ("Hostel_Id",SqlDbType.Int){ Value= Hostel_Id }
          };
        DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectRoomsByHostelId", sqlParameter);
        List<Room_DropDownModel> room_DropDownModels = new List<Room_DropDownModel>();
        foreach (DataRow dr in dt.Rows)
        {
            Room_DropDownModel model = new Room_DropDownModel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Room_Number = dr["Room_Number"].ToString()
            };

            // Add the mapped model to the list
            room_DropDownModels.Add(model);
        }
        return room_DropDownModels;
    }

    public List<Bed_DropDownmodel> GetBedListByRoomID(DatabaseHelper _dbHelper, int Room_ID)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
          {
            new SqlParameter ("Room_ID",SqlDbType.Int){ Value= Room_ID }
          };
        DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectBedsByRoomId", sqlParameter);
        List<Bed_DropDownmodel> Bed_DropDownmodel = new List<Bed_DropDownmodel>();
        foreach (DataRow dr in dt.Rows)
        {
            Bed_DropDownmodel model = new Bed_DropDownmodel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Bed_Number = Convert.ToInt32(dr["Bed_Number"])
            };

            // Add the mapped model to the list
            Bed_DropDownmodel.Add(model);
        }
        return Bed_DropDownmodel;
    }
}
