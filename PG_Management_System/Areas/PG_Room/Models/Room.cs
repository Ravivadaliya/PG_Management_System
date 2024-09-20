using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Hostel.Models;

namespace PG_Management_System.Areas.PG_Room.Models;

public class Room
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Hostel")]
    public int Hostel_ID { get; set; }

    public Hostel Hostel { get; set; }

    [Required]
    public int Room_Number { get; set; }

    [Required]
    public int Room_Capacity { get; set; }

    [Required]
    public int Room_Allocate_Bed { get; set; }

    [Required]
    public int Room_Status { get; set; }

    [Required]
    public int Room_Gender { get; set; }

    [Required]
    public DateTime Room_CreatedDate { get; set; } = DateTime.Now;
}
