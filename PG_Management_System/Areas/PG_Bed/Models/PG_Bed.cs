using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PG_Management_System.Areas.PG_Bed.Models;

public class PG_Bed
{
    [Key]
    public int Id { get; set; }

    [Required]
    [ForeignKey("PG_Room")] // This references the Room_ID from PG_Room table
    public int Room_ID { get; set; }

    [Required]
    public int Bed_Number { get; set; }

    [Required]
    public bool Bed_Status { get; set; } = false; // Default value is false

    [Required]
    public DateTime Bed_CreatedDate { get; set; } = DateTime.Now;

    [Required]
    public DateTime Bed_ModificationDate { get; set; } = DateTime.Now;

}