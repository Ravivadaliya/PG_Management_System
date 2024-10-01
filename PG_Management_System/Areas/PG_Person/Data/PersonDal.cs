using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Person.Data;

public class PersonDal
{
    //public DataTable GetAllPersonByOwnerId(DatabaseHelper _dbHelper)
    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id() }
    //    };
    //    DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Person_SelectAllByOwnerId", sqlParameter);
    //    return dataTable;
    //}
    //public DataTable GetAllPersonByOwnerIdAndPersonId(DatabaseHelper _dbHelper, int Person_Id)
    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("Owner_Id",SqlDbType.Int){ Value= CV.Owner_Id() },
    //        new SqlParameter ("Person_Id",SqlDbType.Int){ Value= Person_Id }
    //    };
    //    DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Person_SelectByOwnerIdandPersonId", sqlParameter);
    //    return dataTable;
    //}
    //public DataTable GetPersonById(DatabaseHelper _dbHelper, int? personId)

    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("Person_ID",SqlDbType.Int){ Value=personId  }
    //    };
    //    DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_GetPersonById", sqlParameter);
    //    return dataTable;
    //}
    //public DataTable GetPersonAllDetailsById(DatabaseHelper _dbHelper, int? personId)
    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("@Person_Id",SqlDbType.Int){ Value=personId  }
    //    };
    //    DataTable dataTable = _dbHelper.ExecuteStoredProcedure("[SP_PG_Person_SelectByPersonId]", sqlParameter);
    //    return dataTable;
    //}

    //public bool InsertPerson(DatabaseHelper _dbHelper, Person person,string Person_Image)
    //{

    //    if (person.File != null)
    //    {

    //        string file_loc = Path.Combine("wwwroot", "upload", person.Hostel_ID.ToString());
    //        string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);


    //        if (!Directory.Exists(full_path))
    //        {
    //            Directory.CreateDirectory(full_path);
    //        }


    //        string file_name_with_path = Path.Combine(full_path, person.File.FileName);


    //        person.Person_Image = Path.Combine("upload", person.Hostel_ID.ToString(), person.File.FileName);


    //        using (var stream = new FileStream(file_name_with_path, FileMode.Create))
    //        {
    //            person.File.CopyTo(stream);
    //        }
    //    }
    //    person.Person_Image = Person_Image;
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //    new SqlParameter("Bed_ID", SqlDbType.Int) { Value = person.Bed_ID },
    //    new SqlParameter("Room_ID", SqlDbType.Int) { Value = person.Room_ID },
    //    new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = person.Hostel_ID },
    //    new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
    //    new SqlParameter("Person_Name", SqlDbType.VarChar) { Value = person.Person_Name },
    //    new SqlParameter("Person_Surname", SqlDbType.VarChar) { Value = person.Person_Surname },
    //    new SqlParameter("Person_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Mobile_Number },
    //    new SqlParameter("Person_Parent_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Parent_Mobile_Number },
    //    new SqlParameter("Person_Email_ID", SqlDbType.VarChar) { Value = person.Person_Email_ID },
    //    new SqlParameter("Person_Gender", SqlDbType.VarChar) { Value = person.Person_Gender },
    //    new SqlParameter("Person_Address", SqlDbType.VarChar) { Value = person.Person_Address },
    //    new SqlParameter("Person_City", SqlDbType.VarChar) { Value = person.Person_City },
    //    new SqlParameter("Person_Profession", SqlDbType.VarChar) { Value = person.Person_Profession },
    //    new SqlParameter("Person_WorkPlace", SqlDbType.VarChar) { Value = person.Person_WorkPlace },
    //    new SqlParameter("Person_WorkPlace_MobileNumber", SqlDbType.VarChar) { Value = person.Person_WorkPlace_MobileNumber },
    //    new SqlParameter("Person_JoningDate", SqlDbType.Date) { Value = person.Person_JoningDate.ToDateTime(TimeOnly.MinValue) },
    //    new SqlParameter("Person_Image", SqlDbType.NVarChar) { Value = person.Person_Image },
    //    new SqlParameter("Person_PassWord", SqlDbType.VarChar) { Value = person.Person_PassWord }
    //    };

    //    int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Insert", sqlParameter);

    //    return value == -1 ? false : true;
    //}

    //public bool UpdatePerson(DatabaseHelper _dbHelper, Person person)
    //{

    //    if (person.File != null)
    //    {

    //        string file_loc = Path.Combine("wwwroot", "upload", person.Hostel_ID.ToString());
    //        string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);


    //        if (!Directory.Exists(full_path))
    //        {
    //            Directory.CreateDirectory(full_path);
    //        }


    //        string file_name_with_path = Path.Combine(full_path, person.File.FileName);

    //        if (File.Exists(file_name_with_path))
    //        {
    //            File.Delete(file_name_with_path);
    //        }

    //        person.Person_Image = Path.Combine("upload", person.Hostel_ID.ToString(), person.File.FileName);


    //        using (var stream = new FileStream(file_name_with_path, FileMode.Create))
    //        {
    //            person.File.CopyTo(stream);
    //        }
    //    }

    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //    new SqlParameter("Id", SqlDbType.Int) { Value = person.Id },
    //    new SqlParameter("Bed_ID", SqlDbType.Int) { Value = person.Bed_ID },
    //    new SqlParameter("Room_ID", SqlDbType.Int) { Value = person.Room_ID },
    //    new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = person.Hostel_ID },
    //    new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
    //    new SqlParameter("Person_Name", SqlDbType.VarChar) { Value = person.Person_Name },
    //    new SqlParameter("Person_Surname", SqlDbType.VarChar) { Value = person.Person_Surname },
    //    new SqlParameter("Person_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Mobile_Number },
    //    new SqlParameter("Person_Parent_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Parent_Mobile_Number },
    //    new SqlParameter("Person_Email_ID", SqlDbType.VarChar) { Value = person.Person_Email_ID },
    //    new SqlParameter("Person_Gender", SqlDbType.VarChar) { Value = person.Person_Gender },
    //    new SqlParameter("Person_Address", SqlDbType.VarChar) { Value = person.Person_Address },
    //    new SqlParameter("Person_City", SqlDbType.VarChar) { Value = person.Person_City },
    //    new SqlParameter("Person_Profession", SqlDbType.VarChar) { Value = person.Person_Profession },
    //    new SqlParameter("Person_WorkPlace", SqlDbType.VarChar) { Value = person.Person_WorkPlace },
    //    new SqlParameter("Person_WorkPlace_MobileNumber", SqlDbType.VarChar) { Value = person.Person_WorkPlace_MobileNumber },
    //    new SqlParameter("Person_JoningDate", SqlDbType.Date) { Value = person.Person_JoningDate.ToDateTime(TimeOnly.MinValue) },
    //    new SqlParameter("Person_Image", SqlDbType.NVarChar) { Value = person.Person_Image },
    //    new SqlParameter("Person_PassWord", SqlDbType.VarChar) { Value = person.Person_PassWord }
    //    };

    //    // Execute the stored procedure
    //    int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Update", sqlParameter);

    //    // Return true if the operation succeeded, false otherwise
    //    return value > 0;
    //}

    //public bool DeletePerson(DatabaseHelper _dbhelper, int person_Id)
    //{
    //    SqlParameter[] sqlParameter = new SqlParameter[]
    //    {
    //        new SqlParameter ("Id",SqlDbType.Int){ Value= person_Id }
    //    };
    //    int value = _dbhelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Delete", sqlParameter);
    //    return value == -1 ? false : true;
    //}


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

    public List<Room_DropDownModel> GetRoomListByHostelIdForbed(DatabaseHelper _dbHelper, int Hostel_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
          {
            new SqlParameter ("Hostel_Id",SqlDbType.Int){ Value= Hostel_Id }
          };
        DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectRoomsByHostelIdForBed", sqlParameter);
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
                Bed_Number = dr["Bed_Number"].ToString(),
            };

            // Add the mapped model to the list
            Bed_DropDownmodel.Add(model);
        }
        return Bed_DropDownmodel;
    }


    public DataTable SelectPersonByEmailPassword(DatabaseHelper _dbHelper, string PersonEmaiul, string PersonPassword)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
         {
           new SqlParameter("@Email_Id",SqlDbType.VarChar){ Value = PersonEmaiul},
           new SqlParameter("@Password",SqlDbType.VarChar){ Value = PersonPassword}
         };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectPersonByEmailPassword", sqlParameter);
        return dataTable;
    }

    public Bed_DropDownmodel GetBedById(DatabaseHelper _dbHelper, int bedId)
    {
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
        new SqlParameter("@Id", SqlDbType.Int) { Value = bedId }
        };

        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Bed_SelectById", sqlParameters);

        if (dataTable.Rows.Count > 0)
        {
            DataRow dr = dataTable.Rows[0];
            Bed_DropDownmodel bed = new Bed_DropDownmodel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Bed_Number = dr["Bed_Number"].ToString()
            };
            return bed;
        }
        return null; // If no bed found
    }

    public Room_DropDownModel GetRoomById(DatabaseHelper _dbHelper, int roomId)
    {
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
        new SqlParameter("@Room_Id", SqlDbType.Int) { Value = roomId }
        };

        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Room_SelectById", sqlParameters);

        if (dataTable.Rows.Count > 0)
        {
            DataRow dr = dataTable.Rows[0];
            return new Room_DropDownModel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Room_Number = dr["Room_Number"].ToString()
            };
        }
        return null;
    }

    public Hostel_DropDownModel GetHostelById(DatabaseHelper _dbHelper, int hostelId)
    {
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
        new SqlParameter("@Hostel_Id", SqlDbType.Int) { Value = hostelId }
        };

        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Hostel_SelectById", sqlParameters);

        if (dataTable.Rows.Count > 0)
        {
            DataRow dr = dataTable.Rows[0];
            return new Hostel_DropDownModel
            {
                Id = Convert.ToInt32(dr["Id"]),
                Hostel_Building_Number = dr["Hostel_Building_Number"].ToString()
            };
        }
        return null;
    }













    public DataTable GetAllPersonByOwnerId(DatabaseHelper _dbHelper)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Person_SelectAllByOwnerId", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GetAllPersonByOwnerIdAndPersonId(DatabaseHelper _dbHelper, int Person_Id)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                new SqlParameter("Person_Id", SqlDbType.Int) { Value = Person_Id }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Person_SelectByOwnerIdandPersonId", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GetPersonById(DatabaseHelper _dbHelper, int? personId)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Person_ID", SqlDbType.Int) { Value = personId }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_GetPersonById", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public DataTable GetPersonAllDetailsById(DatabaseHelper _dbHelper, int? personId)
    {
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("@Person_Id", SqlDbType.Int) { Value = personId }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("[SP_PG_Person_SelectByPersonId]", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {

            return null;
        }
    }

    public bool InsertPerson(DatabaseHelper _dbHelper, Person person)
    {

        try
        {
            if (person.File != null)
            {
                string file_loc = Path.Combine("wwwroot", "upload", person.Hostel_ID.ToString());
                string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);

                if (!Directory.Exists(full_path))
                {
                    Directory.CreateDirectory(full_path);
                }

                string file_name_with_path = Path.Combine(full_path, person.File.FileName);
                person.Person_Image = Path.Combine("upload", person.Hostel_ID.ToString(), person.File.FileName);

                using (var stream = new FileStream(file_name_with_path, FileMode.Create))
                {
                    person.File.CopyTo(stream);
                }
            }
            
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Bed_ID", SqlDbType.Int) { Value = person.Bed_ID },
                new SqlParameter("Room_ID", SqlDbType.Int) { Value = person.Room_ID },
                new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = person.Hostel_ID },
                new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                new SqlParameter("Person_Name", SqlDbType.VarChar) { Value = person.Person_Name },
                new SqlParameter("Person_Surname", SqlDbType.VarChar) { Value = person.Person_Surname },
                new SqlParameter("Person_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Mobile_Number },
                new SqlParameter("Person_Parent_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Parent_Mobile_Number },
                new SqlParameter("Person_Email_ID", SqlDbType.VarChar) { Value = person.Person_Email_ID },
                new SqlParameter("Person_Gender", SqlDbType.VarChar) { Value = person.Person_Gender },
                new SqlParameter("Person_Address", SqlDbType.VarChar) { Value = person.Person_Address },
                new SqlParameter("Person_City", SqlDbType.VarChar) { Value = person.Person_City },
                new SqlParameter("Person_Profession", SqlDbType.VarChar) { Value = person.Person_Profession },
                new SqlParameter("Person_WorkPlace", SqlDbType.VarChar) { Value = person.Person_WorkPlace },
                new SqlParameter("Person_WorkPlace_MobileNumber", SqlDbType.VarChar) { Value = person.Person_WorkPlace_MobileNumber },
                new SqlParameter("Person_JoningDate", SqlDbType.Date) { Value = person.Person_JoningDate.ToDateTime(TimeOnly.MinValue) },
                new SqlParameter("Person_Image", SqlDbType.NVarChar) { Value = person.Person_Image },
                new SqlParameter("Person_PassWord", SqlDbType.VarChar) { Value = person.Person_PassWord }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Insert", sqlParameter);
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

    public bool UpdatePerson(DatabaseHelper _dbHelper, Person person, string Person_Image)
    {
        try
        {
            if (person.File != null)
            {
                string file_loc = Path.Combine("wwwroot", "upload", person.Hostel_ID.ToString());
                string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);

                if (!Directory.Exists(full_path))
                {
                    Directory.CreateDirectory(full_path);
                }

                string file_name_with_path = Path.Combine(full_path, person.File.FileName);

                if (File.Exists(file_name_with_path))
                {
                    File.Delete(file_name_with_path);
                }

                person.Person_Image = Path.Combine("upload", person.Hostel_ID.ToString(), person.File.FileName);

                using (var stream = new FileStream(file_name_with_path, FileMode.Create))
                {
                    person.File.CopyTo(stream);
                }
            }
            else
            {
                person.Person_Image = Person_Image;
            }
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("Id", SqlDbType.Int) { Value = person.Id },
                new SqlParameter("Bed_ID", SqlDbType.Int) { Value = person.Bed_ID },
                new SqlParameter("Room_ID", SqlDbType.Int) { Value = person.Room_ID },
                new SqlParameter("Hostel_ID", SqlDbType.Int) { Value = person.Hostel_ID },
                new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                new SqlParameter("Person_Name", SqlDbType.VarChar) { Value = person.Person_Name },
                new SqlParameter("Person_Surname", SqlDbType.VarChar) { Value = person.Person_Surname },
                new SqlParameter("Person_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Mobile_Number },
                new SqlParameter("Person_Parent_Mobile_Number", SqlDbType.VarChar) { Value = person.Person_Parent_Mobile_Number },
                new SqlParameter("Person_Email_ID", SqlDbType.VarChar) { Value = person.Person_Email_ID },
                new SqlParameter("Person_Gender", SqlDbType.VarChar) { Value = person.Person_Gender },
                new SqlParameter("Person_Address", SqlDbType.VarChar) { Value = person.Person_Address },
                new SqlParameter("Person_City", SqlDbType.VarChar) { Value = person.Person_City },
                new SqlParameter("Person_Profession", SqlDbType.VarChar) { Value = person.Person_Profession },
                new SqlParameter("Person_WorkPlace", SqlDbType.VarChar) { Value = person.Person_WorkPlace },
                new SqlParameter("Person_WorkPlace_MobileNumber", SqlDbType.VarChar) { Value = person.Person_WorkPlace_MobileNumber },
                new SqlParameter("Person_JoningDate", SqlDbType.Date) { Value = person.Person_JoningDate.ToDateTime(TimeOnly.MinValue) },
                new SqlParameter("Person_Image", SqlDbType.NVarChar) { Value = person.Person_Image },
                new SqlParameter("Person_PassWord", SqlDbType.VarChar) { Value = person.Person_PassWord }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Update", sqlParameter);
            return value > 0;
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

    public bool DeletePerson(DatabaseHelper _dbHelper, int? personId)
    {

        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                new SqlParameter("ID", SqlDbType.Int) { Value = personId }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Person_Delete", sqlParameter);
            return value > 0;
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
