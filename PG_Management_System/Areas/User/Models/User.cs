using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Owner.Models;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Room.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PG_Management_System.Areas.User.Models;

public class User
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Person")]
    public int Person_ID { get; set; }

    public Person Person { get; set; }

    [ForeignKey("Room")]
    public int Room_ID { get; set; }

    public Room Room { get; set; }

    [ForeignKey("Hostel")]
    public int Hostel_ID { get; set; }

    public Hostel Hostel { get; set; }

    [ForeignKey("Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [Required]
    [StringLength(50)]
    public string Issue_Description { get; set; }

    [Required]
    [StringLength(100)]
    public string Issue_ImgPath { get; set; }

    [Required]
    public bool Issue_Status { get; set; }

    public DateOnly CreatedDate { get; set; }
    public DateOnly Modificationdate { get; set; }
}
