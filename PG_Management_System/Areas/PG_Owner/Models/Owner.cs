using System.ComponentModel.DataAnnotations;

namespace PG_Management_System.Areas.PG_Owner.Models;

public class Owner
{
    [Key]
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner_Surname { get; set; }

    [Required]
    public int Owner_Mobile_Number { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Owner_EmailId { get; set; }

    [Required]
    [StringLength(10)]
    public string Owner_Gender { get; set; }


    [Required]
    [StringLength(100)]
    public string Owner_Address { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner_City { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner_State { get; set; }

    [Required]
    public bool Owner_IsActive { get; set; }

    [Required]
    [StringLength(50)]
    public string Owner_PassWord { get; set; }

    public DateOnly Owner_CreatedDate { get; set; }

    public DateOnly Owner_ModificationDate { get; set; }
}
