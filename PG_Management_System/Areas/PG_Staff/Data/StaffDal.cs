using DatabaseHelperLibrary;
using System.Data.SqlClient;
using System.Data;
using PG_Management_System.BAL;
using PG_Management_System.Areas.PG_Staff.Models;

namespace PG_Management_System.Areas.PG_Staff.Data
{
    public class StaffDal
    {
        public DataTable GetStaffById(DatabaseHelper _dbHelper, int? StaffId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Id", SqlDbType.Int) { Value = StaffId }
                };
                return _dbHelper.ExecuteStoredProcedure("SP_PG_Staff_SelectById", sqlParameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetAllStaffByOwnerId(DatabaseHelper _dbHelper)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() }
                };
                return _dbHelper.ExecuteStoredProcedure("SP_PG_Staff_SelectByOwnerId", sqlParameter);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool InsertStaffData(Staff staff, DatabaseHelper _dbHelper)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = CV.Owner_Id() },
                    new SqlParameter("@Staff_Name", SqlDbType.VarChar) { Value = staff.Staff_Name },
                    new SqlParameter("@Staff_Surname", SqlDbType.VarChar) { Value = staff.Staff_Surname },
                    new SqlParameter("@Staff_Mobile_Number", SqlDbType.VarChar) { Value = staff.Staff_Mobile_Number },
                    new SqlParameter("@Staff_Address", SqlDbType.VarChar) { Value = staff.Staff_Address },
                    new SqlParameter("@Staff_Gender", SqlDbType.VarChar) { Value = staff.Staff_Gender },
                    new SqlParameter("@Staff_City", SqlDbType.VarChar) { Value = staff.Staff_City }
                };

                int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Insert", sqlParameter);
                if (value == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteStaff(DatabaseHelper _dbHelper, int staffId)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Id", SqlDbType.Int) { Value = staffId }
                };

                int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Delete", sqlParameter);
                if (value == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool UpdateStaffData(DatabaseHelper _dbHelper, Staff staff, ref string errorMessage)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("@Id", SqlDbType.Int) { Value = staff.Id },
                    new SqlParameter("@Owner_ID", SqlDbType.Int) { Value = staff.Owner_ID },
                    new SqlParameter("@Staff_Name", SqlDbType.VarChar) { Value = staff.Staff_Name },
                    new SqlParameter("@Staff_Surname", SqlDbType.VarChar) { Value = staff.Staff_Surname },
                    new SqlParameter("@Staff_Mobile_Number", SqlDbType.VarChar) { Value = staff.Staff_Mobile_Number },
                    new SqlParameter("@Staff_Address", SqlDbType.VarChar) { Value = staff.Staff_Address },
                    new SqlParameter("@Staff_Gender", SqlDbType.VarChar) { Value = staff.Staff_Gender },
                    new SqlParameter("@Staff_City", SqlDbType.VarChar) { Value = staff.Staff_City }
                };

                int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_PG_Staff_Update", sqlParameter);
                if (value == -1)
                {
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
