﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;

namespace PG_Management_System.Areas.PG_Hostel.Models;

public class Hostel
{
    [Key]
    public int Id { get; set; }

    [ForeignKey("Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [StringLength(20)]
    public string Building_Number { get; set; }

    [Required]
    [StringLength(100)]
    public string Hostel_Address { get; set; }

    [Required]
    public DateTime Hostel_CreatedDate { get; set; } = DateTime.Now;
}
