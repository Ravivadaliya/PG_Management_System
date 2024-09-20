﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Owner.Models;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.Areas.PG_Person.Models;

namespace PG_Management_System.Areas.PG_Issues.Models;

public class Issues
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

    [StringLength(50)]
    public string Issue_ImgPath { get; set; }

    [Required]
    public bool Issue_Status { get; set; }
}
