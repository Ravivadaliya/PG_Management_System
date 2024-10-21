using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization.Formatters;

namespace PG_Management_System.Areas.PG_Payments.Models
{
    public class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }


        [Required(ErrorMessage = "Person ID is required.")]
        [ForeignKey("PGPerson")]
        public int Person_Id { get; set; }


        [Required(ErrorMessage = "Owner ID is required.")]
        [ForeignKey("PGOwner")]
        public int Owner_Id { get; set; }


        [Required]
        public IFormFile File { get; set; }


        [Required(ErrorMessage = "Payment Image is required.")]
        [StringLength(100, ErrorMessage = "Payment Image path cannot exceed 100 characters.")]
        public string Payment_Image { get; set; }

        
        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime Payment_CreationDate { get; set; } 


        [Required(ErrorMessage = "Payment Status is required.")]
        public bool PaymentStatus { get; set; }


        public string Payment_Amount { get; set; }

        public DateTime? Payment_ReceivedDate { get; set; }

    }

    public class PendingPaymentViewModel
    {
        public int ID { get; set; }
        public string Person_Name { get; set; }
        public string Person_Surname { get; set; }
        public string Person_Email_ID { get; set; }
        public string Person_Mobile_Number { get; set; }
        public string Bed_Number { get; set; }
        public DateTime Payment_CreationDate { get; set; }
        public int Payment_Status { get; set; }
        public decimal Room_Rent { get; set; }
        public string PG_Number { get; set; }
    }



}


