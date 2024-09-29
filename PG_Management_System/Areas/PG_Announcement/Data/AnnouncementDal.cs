using DatabaseHelperLibrary;
using Microsoft.Practices.EnterpriseLibrary.Data;
using PG_Management_System.Areas.PG_Announcement.Models;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;
namespace PG_Management_System.Areas.PG_Announcement.Data;
public class AnnouncementDal
{
    public bool InsertAnnouncement(DatabaseHelper _dbHelper, Announcement announcement)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
            new SqlParameter("Announcement_Title",SqlDbType.VarChar){Value = announcement.Announcement_Title},
            new SqlParameter("Announcement_Message",SqlDbType.VarChar){Value = announcement.Announcement_Message},
            new SqlParameter("Hostel_ID",SqlDbType.Int){Value = announcement.Hostel_ID},
            new SqlParameter("Owner_ID",SqlDbType.Int){Value = CV.Owner_Id()},
        };


        // Execute the stored procedure and get the Announcement_ID
        object result = _dbHelper.ExecuteStoredProcedureScalar("Sp_PG_Announcement_Insert", sqlParameter);

        if (result == null)
        {
            return false;
        }

        int announcementId = Convert.ToInt32(result);


        // Get the list of users associated with the given Hostel_ID
        List<int> userIds = GetUsersByHostelId(_dbHelper, announcement.Hostel_ID);

        // Insert notifications for each user
        foreach (int userId in userIds)
        {
            SqlParameter[] notificationParameters = new SqlParameter[]
            {
            new SqlParameter("Person_ID", SqlDbType.Int) { Value = userId },
            new SqlParameter("Announcement_ID", SqlDbType.Int) { Value = announcementId },
            new SqlParameter("seen", SqlDbType.Bit) { Value = false }
            };

            _dbHelper.ExecuteStoredProcedureNonQuery("Sp_PG_UserNotification_Insert", notificationParameters);
        }

        return true;
    }

    // Helper method to get users by Hostel_ID
    private List<int> GetUsersByHostelId(DatabaseHelper _dbHelper, int hostelId)
    {
        SqlParameter[] sqlParameter = new SqlParameter[]
        {
        new SqlParameter("HostelId", SqlDbType.Int) { Value = hostelId }
        };

        DataTable dtUsers = _dbHelper.ExecuteStoredProcedure("Sp_GetPersonByHostelId", sqlParameter);
        List<int> userIds = new List<int>();

        foreach (DataRow row in dtUsers.Rows)
        {
            userIds.Add(Convert.ToInt32(row["ID"]));
        }

        return userIds;
    }

}
