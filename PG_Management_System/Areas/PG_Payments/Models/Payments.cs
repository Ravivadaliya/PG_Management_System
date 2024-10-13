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

        [Required]

        public DateTime Payment_DueDate { get; set; }

        [Required(ErrorMessage = "Payment Date is required.")]
        public DateTime PaymentDate { get; set; } = DateTime.Now;


        [Required(ErrorMessage = "Payment Status is required.")]
        public bool PaymentStatus { get; set; }


        public string Payment_Amount { get; set; }
    }
}
