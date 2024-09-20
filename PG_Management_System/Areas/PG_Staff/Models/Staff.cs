using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;

namespace PG_Management_System.Areas.PG_Staff.Models;

public class Staff
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("PG_Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [Required]
    [StringLength(50)]
    public string Staff_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Staff_Surname { get; set; }

    [Required]
    public int Staff_Mobile_Number { get; set; }

    [Required]
    [StringLength(100)]
    public string Staff_Address { get; set; }

    [Required]
    public DateTime Owner_CreatedDate { get; set; } = DateTime.Now;
}
