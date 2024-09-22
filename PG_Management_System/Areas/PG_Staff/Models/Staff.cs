using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;

namespace PG_Management_System.Areas.PG_Staff.Models;

public class Staff
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Owner")]
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
    public string Staff_Gender { get; set; }

    [Required]
    [StringLength(100)]
    public string Staff_Address { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Staff_City { get; set; }

    public DateOnly Owner_CreatedDate { get; set; }
    public DateOnly Owner_ModificationDate { get; set; }
}
