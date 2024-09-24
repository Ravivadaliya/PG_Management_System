using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Hostel.Models;

namespace PG_Management_System.Areas.PG_Room.Models;

public class Room
{
    [Key]
    public int? Id { get; set; }

    [ForeignKey("Hostel")]
    public int Hostel_ID { get; set; }

    public Hostel Hostel { get; set; }

    [Required]
    public string Room_Number { get; set; }
    
    [Required]
    public string Room_GenderAllowed { get; set; }
    
    [Required]
    public int Room_SharingType { get; set; }

    [Required]
    public int Room_AllowcateBed { get; set; }

    [Required]
    public string Room_Type { get; set; }
    public DateOnly Room_CreatedDate { get; set; }
    public DateOnly Room_ModificationDate { get; set; }
}
public class Room_DropDownModel
{
    public int Id { get; set; }
    public string Room_Number { get; set; }
}