using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;
using PG_Management_System.Areas.PG_Hostel.Models;

namespace PG_Management_System.Areas.PG_Announcement.Models;

public class Announcement
{
    [Key]
    public int ID { get; set; }

    [ForeignKey("Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [ForeignKey("PG_Hostel")]
    public int Hostel_ID { get; set; }

    public Hostel Hostel { get; set; }

    [Required]
    [StringLength(255)]
    public string Announcement_Title { get; set; }

    [Required]
    public string Announcement_Message { get; set; }

    [Required]
    public DateTime created_at { get; set; } = DateTime.Now;
}
