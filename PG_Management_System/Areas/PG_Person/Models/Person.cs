﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.Areas.PG_Hostel.Models;
using PG_Management_System.Areas.PG_Owner.Models;
using PG_Management_System.Areas.PG_Bed.Models;

namespace PG_Management_System.Areas.PG_Person.Models;

public class Person
{
    [Key]
    public int? Id { get; set; }


    //[ForeignKey("Room")]
    //public int Bed_ID { get; set; }

    //public Bed Bed { get; set; }


    //[ForeignKey("Room")]
    //public int Room_ID { get; set; }

    //public Room Room { get; set; }

    //[ForeignKey("Hostel")]
    //public int Hostel_ID { get; set; }

    //public Hostel Hostel { get; set; }

    [ForeignKey("Owner")]
    public int Owner_ID { get; set; }

    public Owner Owner { get; set; }

    [Required]
    [StringLength(50)]
    public string Person_Name { get; set; }

    [Required]
    [StringLength(50)]
    public string Person_Surname { get; set; }

    [Required]
    [StringLength(10), MinLength(10)]
    public string Person_Mobile_Number { get; set; }

    [Required]
    [StringLength(10), MinLength(10)]
    public string Person_Parent_Mobile_Number { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string Person_Email_ID { get; set; }

    [Required]
    [StringLength(100)]
    public string Person_Address { get; set; }

    [Required]
    [StringLength(10)]
    public string Person_Gender { get; set; }

    [Required]
    [StringLength(100)]
    public string Person_City { get; set; }

    [Required]
    [StringLength(50)]
    public string Person_Profession { get; set; }

    [Required]
    public DateOnly Person_JoningDate { get; set; }

    [StringLength(50)]
    public string Person_WorkPlace { get; set; }


    [Required]
    [StringLength(10), MinLength(10)]
    public string Person_WorkPlace_MobileNumber { get; set; }

    [Required]
    [StringLength(50)]
    public string Person_PassWord { get; set; }

    //[Required]
    //[StringLength(20)]
    //public string Person_PaymentMode { get; set; }

    [Required]
    [StringLength(20)]
    public string Person_AadharCard { get; set; }

    [NotMapped]
    public IFormFile? File { get; set; }

    //[Required]
    //[StringLength(120)]
    public string Person_Image { get; set; }
    
    public DateOnly Person_CreatedDate { get; set; }
    public DateOnly Person_ModificationDate { get; set; }
}

public class PaymentPerson
{
    [Key]
    public int Id { get; set; }


    [ForeignKey("Room")]
    public int Bed_ID { get; set; }

    public Bed Bed { get; set; }


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
    [StringLength(20)]
    public string Payment_Cycle { get; set; }

}


public class PersonWithBedDetails
{

    public int ID { get; set; }

    public string PersonName { get; set; }

    public string PersonSurname { get; set; }

    public string PersonProfession { get; set; }

    public string PersonMobile_Number { get; set; }

    public string Person_Parent_Mobile_Number { get; set; }

    public string Bed_Number { get; set; }

    public string RoomType { get; set; }
    public string RoomRent { get; set; }


}