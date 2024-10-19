using DatabaseHelperLibrary;
using Hangfire.Common;
using PG_Management_System.Areas.PG_Payments.Models;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Payments.Data
{
    public class PaymentsDal
    {
        public DataTable GetPenddingPaymentListByOwnerId(DatabaseHelper _dbHelper, int Hostel_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                    new SqlParameter("Hostel_Id", SqlDbType.Int) { Value = Hostel_Id }
                };
                DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Payment_SelectPenddingPaymentByOwnerIdHoatelId", sqlParameter);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataTable GetCompletePaymentListByOwnerId(DatabaseHelper _dbHelper, int Hostel_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                    new SqlParameter("Hostel_Id", SqlDbType.Int) { Value = Hostel_Id }
                };
                DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Payment_SelectCompletePaymentByOwnerIdHoatelId", sqlParameter);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetLatestPaymentByPersonId(DatabaseHelper _dbHelper, int personId)
        {

            // Create SQL parameters
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Person_Id", SqlDbType.Int) { Value = personId }
            };

            DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectPersonByLatestPayment", sqlParameters);

            return dt;
        }
        public bool SavePayment(DatabaseHelper _dbHelper, Payments payments)
        {

            // Create SQL parameters
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Person_Id", SqlDbType.Int) { Value = payments.Person_Id },
            new SqlParameter("@Owner_Id", SqlDbType.Int) { Value = payments.Owner_Id },
            new SqlParameter("@Payment_Status", SqlDbType.Int) { Value = payments.PaymentStatus },
            new SqlParameter("@Payment_CreationDate", SqlDbType.Date) { Value = payments.Payment_CreationDate }
            };

            int value = _dbHelper.ExecuteStoredProcedureNonQuery("SP_InsertPaymentByAdmin", sqlParameters);
            return value == -1 ? false : true;

        }

        public DataTable selectPaymentListFromPersonID(DatabaseHelper _dbHelper)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
            new SqlParameter("@Person_Id", SqlDbType.Int) { Value = CV.Person_Id() }
            };

            DataTable dt = _dbHelper.ExecuteStoredProcedure("SP_SelectPaymentHistoryByPersonId", sqlParameters);

            return dt;
        }

        public bool UpdatePaymentStatus(DatabaseHelper _dbHelper,int Payment_Id)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("ID",SqlDbType.Int){Value = Payment_Id},
                new SqlParameter("Payment_Status",SqlDbType.Bit){Value = true},
                new SqlParameter("Payment_ReceivedDate",SqlDbType.Date){Value = DateTime.Now.Date}
            };

            int rvalue = _dbHelper.ExecuteStoredProcedureNonQuery("SP_UpdatePGPaymentStatus", sqlParameters);
            return rvalue ==-1?false:true;
        }

    }
}
