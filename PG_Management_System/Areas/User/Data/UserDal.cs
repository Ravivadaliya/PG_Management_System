using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.Areas.User.Models;
using System.Linq.Expressions;
using PG_Management_System.Areas.PG_Payments.Models;
using PG_Management_System.Areas.PG_Person.Data;

namespace PG_Management_System.Areas.User.Data;

public class UserDal
{
    public DataTable GetAllissueByRoomId(DatabaseHelper _dbHelper, int room_Id, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                  new SqlParameter("@Room_Id", SqlDbType.Int) { Value = room_Id }
            };
            DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectIssueListByRoomId", sqlParameter);
            return dataTable;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error fetching issues: {ex.Message}";
            return new DataTable(); // Return an empty DataTable in case of an error
        }
    }

    public bool InsertIssue(DatabaseHelper _dbHelper, Models.User userIssue, out string errorMessage)
    {
        errorMessage = string.Empty;
        try
        {
            SqlParameter[] sqlParameter = new SqlParameter[]
            {
                    new SqlParameter("@Person_ID", SqlDbType.Int) { Value = userIssue.Person_ID },
                    new SqlParameter("@Room_ID", SqlDbType.Int) { Value = userIssue.Room_ID },
                    new SqlParameter("@Hostel_ID", SqlDbType.Int) { Value = userIssue.Hostel_ID },
                    new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = userIssue.Owner_ID },
                    new SqlParameter("@Issue_ImgPath", SqlDbType.VarChar) { Value = userIssue.Issue_ImgPath },
                    new SqlParameter("@Issue_Status", SqlDbType.Bit) { Value = userIssue.Issue_Status },
                    new SqlParameter("@Issue_Description", SqlDbType.VarChar) { Value = userIssue.Issue_Description }
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Isssue_Insert", sqlParameter);
            return value != -1;
        }
        catch (Exception ex)
        {
            errorMessage = $"Error inserting issue: {ex.Message}";
            return false;
        }
    }

    public DataTable GetUserNewNotification(DatabaseHelper _dbHelper, int Person_Id)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("@Person_Id",SqlDbType.Int){ Value= Person_Id}
        };
        DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_SelectuserNotificationbyPersonId", sqlParameter);
        return dataTable;
    }
    public bool InsertIssue(DatabaseHelper _dbHelper, Models.User userIssue)
    {
        try
        {

            SqlParameter[] sqlParameter = new SqlParameter[]
            {
            new SqlParameter ("@Person_ID",SqlDbType.Int){ Value= userIssue.Person_ID},
            new SqlParameter ("@Room_ID",SqlDbType.Int){ Value= userIssue.Room_ID},
            new SqlParameter ("@Hostel_ID",SqlDbType.Int){ Value= userIssue.Hostel_ID},
            new SqlParameter ("@Owner_ID",SqlDbType.Int){ Value= userIssue.Owner_ID},
            new SqlParameter ("@Issue_ImgPath",SqlDbType.VarChar){ Value= userIssue.Issue_ImgPath},
            new SqlParameter ("@Issue_Status",SqlDbType.Bit){ Value= userIssue.Issue_Status},
            new SqlParameter ("@Issue_Description",SqlDbType.VarChar){ Value= userIssue.Issue_Description}
            };
            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Isssue_Insert", sqlParameter);
            return value == -1 ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public bool UpdateNotificationStatus(DatabaseHelper _dbHelper)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter ("Person_Id",SqlDbType.Int){ Value= CV.Person_Id()},
        };
        int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_UpdateUsernotificationStatus", sqlParameter);
        return value == -1 ? false : true;
    }


    public bool InsertPayments(DatabaseHelper _dbHelper, Payments payments)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            DataTable dataTable = personDal.GetPersonById(_dbHelper, Convert.ToInt32(CV.Person_Id()));

            if (dataTable.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    payments.Person_Id = Convert.ToInt32(CV.Person_Id());
                    payments.Owner_Id = Convert.ToInt32(dataRow["Owner_Id"]);
                    payments.PaymentStatus = false;
                }
            }
            else
                return false;


            if (payments.File != null)
            {
                string file_loc = Path.Combine("wwwroot", "upload", "Payments");
                string full_path = Path.Combine(Directory.GetCurrentDirectory(), file_loc);

                if (!Directory.Exists(full_path))
                {
                    Directory.CreateDirectory(full_path);
                }

                if (File.Exists(full_path))
                {
                    File.Delete(full_path);
                }

                string file_name_with_path = Path.Combine(full_path, payments.File.FileName);
                payments.Payment_Image = Path.Combine("upload", "Payments", payments.File.FileName);

                using (var stream = new FileStream(file_name_with_path, FileMode.Create))
                {
                    payments.File.CopyTo(stream);
                }
            }

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("Person_Id",SqlDbType.Int){Value=payments.Person_Id},
                new SqlParameter("Owner_Id",SqlDbType.Int){Value=payments.Owner_Id},
                new SqlParameter("Payment_Image",SqlDbType.VarChar){Value = payments.Payment_Image},
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_InsertPGPaymentFromUser", sqlParameters);
            return value==-1 ? false : true;
        }
        catch (Exception ex)
        {
            return false;
        }

    }
}
