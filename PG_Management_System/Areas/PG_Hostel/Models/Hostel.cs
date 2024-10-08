using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;

namespace PG_Management_System.Areas.PG_Hostel.Models;

public class Hostel
{
    [Key]
    public int? Id { get; set; }

    [Required]
    [ForeignKey("Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [Required]
    [StringLength(20)]
    public string PG_Number { get; set; }

    [Required]
    [StringLength(20)]
    public string Hostel_MinimumPayment { get; set; }
    [Required]
    [StringLength(10)]
    public string Hostel_Property_Category { get; set; }
    [Required]
    [StringLength(10)]
    public string Hostel_Type { get; set; }

    [Required]
    [StringLength(100)]
    public string Hostel_Address { get; set; }

    [Required]
    [StringLength(30)]
    public string Hostel_Floor { get; set; }

    [Required]
    [StringLength(30)]
    public string Hostel_Society { get; set; }

    [Required]
    [StringLength(10)]
    public string Hostel_Gender{ get; set; }


    public DateOnly Hostel_CreatedDate { get; set; }
    public DateOnly Hostel_ModificationDate { get; set; }
}

public class Hostel_DropDownModel
{
    public int Id { get; set; }
    public string PG_Number { get; set; }
}