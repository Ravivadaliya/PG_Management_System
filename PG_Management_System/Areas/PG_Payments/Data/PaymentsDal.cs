using DatabaseHelperLibrary;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;

namespace PG_Management_System.Areas.PG_Payments.Data
{
    public class PaymentsDal
    {
        public DataTable GetPaymentListByOwnerId(DatabaseHelper _dbHelper,int Hostel_Id)
        {
            try
            {
                SqlParameter[] sqlParameter = new SqlParameter[]
                {
                    new SqlParameter("Owner_Id", SqlDbType.Int) { Value = CV.Owner_Id() },
                    new SqlParameter("Hostel_Id", SqlDbType.Int) { Value = Hostel_Id }
                };
                DataTable dataTable = _dbHelper.ExecuteStoredProcedure("SP_PG_Payment_SelectByOwnerIdHoatelId", sqlParameter);
                return dataTable;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
