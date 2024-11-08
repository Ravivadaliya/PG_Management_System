﻿using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;
using OfficeOpenXml.Table;
using OfficeOpenXml;
using PG_Management_System.Areas.PG_Bed.Models;
using PG_Management_System.Areas.PG_Payments.Data;
using PG_Management_System.Areas.PG_Payments.Models;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using PG_Management_System.Areas.PG_Room.Models;
using PG_Management_System.BAL;
using System.Data;
using System.Data.SqlClient;
using PG_Management_System.Helper;
using Microsoft.Extensions.Caching.Memory;

namespace PG_Management_System.Areas.PG_Person.Controllers;

[CheckAccess]
[Area("PG_Person")]
[Route("Person")]
public class PG_PersonController : Controller
{

    private readonly IMemoryCache _cache;
    private readonly DatabaseHelper _dbHelper;
    public static string Person_Image = "";
    private readonly AESEncryptionHelper _aesencryptionHelper;
    public PG_PersonController(DatabaseHelper dbHelper, AESEncryptionHelper aesencryptionHelper, IMemoryCache cache)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
        _aesencryptionHelper = aesencryptionHelper;
        _cache = cache;
    }

    [HttpGet("AllPersonList")]
    public IActionResult AllPersonList()
    {

        const string cacheKey = "PersonDataCache";
        DataTable dataTable;
        //_cache.Remove("PersonDataCache");
        if (!_cache.TryGetValue(cacheKey, out dataTable))
        {
            PersonDal personDal = new PersonDal();
            dataTable = personDal.GetAllPersonByOwnerId(_dbHelper);

            // Set cache options (optional)
            var cacheExpirationOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30), // cache duration
                Priority = CacheItemPriority.Normal
            };

            _cache.Set(cacheKey, dataTable, cacheExpirationOptions);
        }

        return View("AllPersonList", dataTable);


    }



    public IActionResult AddEditPerson()
    {
        try
        {
            return View();
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }

    [HttpGet("AddEdit")]
    public IActionResult Add(int? Person_Id)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            Person person = new Person();

            if (Person_Id != null)
            {
                DataTable dataTable = personDal.GetPersonById(_dbHelper, Person_Id);

                if (dataTable.Rows.Count > 0)
                {
                    foreach (DataRow dr in dataTable.Rows)
                    {
                        person.Id = Convert.ToInt32(dr["Id"]);
                        person.Person_Name = dr["Person_Name"].ToString();
                        person.Person_Surname = dr["Person_Surname"].ToString();
                        person.Person_Mobile_Number = dr["Person_Mobile_Number"].ToString();
                        person.Person_Gender = dr["Person_Gender"].ToString();
                        person.Person_Address = dr["Person_Address"].ToString();
                        person.Person_Parent_Mobile_Number = dr["Person_Parent_Mobile_Number"].ToString();
                        person.Person_Email_ID = dr["Person_Email_ID"].ToString();
                        person.Person_City = dr["Person_City"].ToString();
                        person.Person_Profession = dr["Person_Profession"].ToString();
                        person.Person_WorkPlace = dr["Person_WorkPlace"].ToString();
                        person.Person_WorkPlace_MobileNumber = dr["Person_WorkPlace_MobileNumber"].ToString();
                        person.Person_JoningDate = DateOnly.FromDateTime(Convert.ToDateTime(dr["Person_JoningDate"]));
                        person.Person_Image = dr["Person_Image"].ToString();

                        person.Person_AadharCard = dr["Person_AadharCard"].ToString();
                        person.Person_PassWord = dr["Person_PassWord"].ToString();
                    }
                    Person_Image = person.Person_Image;
                    return View("AddEditPerson", person);
                }
            }
            return View("AddEditPerson");
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }
    [HttpGet("PersonDetails")]
    public IActionResult PersonDetails(int Person_Id)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            DataTable dataTable = personDal.GetAllPersonByOwnerIdAndPersonId(_dbHelper, Person_Id);

            return View("PersonDetails", dataTable);
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }


    [HttpGet("PersonWithBedDetails")]
    public IActionResult PersonWithBedDetails(int PG_ID)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            DataTable dataTable = personDal.GetAllPersonWithBedByHostelId(_dbHelper, PG_ID);
            return View("PersonDetailsByRoom", dataTable);
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }

    [HttpPost("SavePerson")]
    public IActionResult SavePerson(Person person)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            if (person.Id == null)
            {
                if (person.Person_Image==null)
                {
                    TempData["Message"] = "Please Upload Image Also!";
                    TempData["AlertType"] = "error";
                    return RedirectToAction("AllPersonList");
                }

                if (personDal.InsertPerson(_dbHelper, person))
                {
                    TempData["Message"] = "Person data inserted successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Error in insert person!";
                    TempData["AlertType"] = "error";
                }
            }
            else
            {
                if (personDal.UpdatePerson(_dbHelper, person, Person_Image))
                {
                    TempData["Message"] = "Person data Update successfully!";
                    TempData["AlertType"] = "success";
                }
                else
                {
                    TempData["Message"] = "Error in update person!";
                    TempData["AlertType"] = "error";
                }
            }
            return RedirectToAction("AllPersonList");
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }


    [HttpPost("personDelete")]
    public IActionResult personDelete(int Person_Id)
    {
        try
        {
            PersonDal person = new PersonDal();
            if (person.DeletePerson(_dbHelper, Person_Id))
            {
                TempData["Message"] = "Person deleted successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["Message"] = "Failed to delete person.";
                TempData["AlertType"] = "error";
            }
            return RedirectToAction("AllPersonList");
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred: {ex.Message}";
            TempData["AlertType"] = "error";
            return RedirectToAction("AllPersonList");
        }
    }


    #region PG_Room_RoomDropDownByHostelId
    public IActionResult PG_Room_RoomDropDownByHostelId(int Hostel_Id)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            ViewBag.RoomList = personDal.GetRoomListByHostelId(_dbHelper, Hostel_Id);
            var roomModel = ViewBag.RoomList;
            return Json(roomModel);
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred";
            TempData["AlertType"] = "error";
            return Json(null);
        }
    }
    #endregion

    public IActionResult PG_Bed_BedDropDownByRoomId(int Room_ID)
    {
        try
        {
            PersonDal personDal = new PersonDal();
            ViewBag.BedList = personDal.GetBedListByRoomID(_dbHelper, Room_ID);
            var bedModel = ViewBag.BedList;
            return Json(bedModel);
        }
        catch (Exception ex)
        {
            TempData["Message"] = $"An unexpected error occurred";
            TempData["AlertType"] = "error";
            return Json(null);
        }
    }


    [HttpGet("SelectPaymentHistoryByPerson")]
    public IActionResult SelectPaymentHistoryByPerson(int personId)
    {
        try
        {
            PersonDal personDal = new PersonDal();

            List<Payments> Paymenthistory = personDal.selectPaymentHistoryByPersonId(_dbHelper, personId);

            return Ok(Paymenthistory);
        }
        catch (Exception ex)
        {
            return Json(new { success = false, message = "Error Fetch Transaction History" });
        }
    }



    //public IActionResult SelectPersonByHostelNumber()
    //{
    //    try
    //    {

    //    }
    //    catch (Exception ex)
    //    {
    //        return Json(new { success = false,message = "Error geting persons" })
    //    }
    //}



    #region Automatic payment genrate


    [HttpGet("GeneratePaymentRequests")]
    public void GeneratePaymentRequests()
    {
        //Thread.Sleep(20000);
        PersonDal personDal = new PersonDal();
        DataTable personTable = personDal.GetAllPersonByBedAssign(_dbHelper);

        foreach (DataRow dr in personTable.Rows)
        {
            PaymentPerson person = MapDataRowToPerson(dr); // Map DataRow to Person object

            // Check if a new payment request should be generated based on the person's plan
            if (ShouldGeneratePaymentRequest(person))
            {
                // Create a new payment request for the person
                Payments payment = new Payments
                {
                    Person_Id = person.Id,
                    Owner_Id = person.Owner_ID,
                    PaymentStatus = false,
                    Payment_CreationDate = DateTime.Now.Date // Set to the first date of the current month
                };

                // Save payment request to the database
                SavePaymentRequestToDatabase(payment);
            }
        }
    }

    // Method to map DataRow to Person object
    private PaymentPerson MapDataRowToPerson(DataRow dr)
    {
        return new PaymentPerson
        {
            Id = Convert.ToInt32(dr["Id"]),
            Owner_ID = Convert.ToInt32(dr["Owner_ID"]),
            Payment_Cycle = dr["Payment_Cycle"].ToString()
        };
    }

    // Check if a payment request should be generated
    private bool ShouldGeneratePaymentRequest(PaymentPerson person)
    {

        Payments latestPayment = GetLatestPaymentForPerson(person.Id);

        if (latestPayment == null)
        {
            // No previous payment exists, create a new one
            return true;
        }

        // Determine if the payment request should be generated based on the person's plan
        switch (person.Payment_Cycle.ToLower())
        {
            case "daily":
                return latestPayment.Payment_CreationDate.AddDays(1) <= DateTime.Now.Date;

            case "weekly":
                return latestPayment.Payment_CreationDate.AddDays(7) <= DateTime.Now.Date;

            case "monthly":
                return latestPayment.Payment_CreationDate.AddMonths(1) <= DateTime.Now.Date;

            default:
                return false;
        }
    }

 

    // Save payment request to the database
    private void SavePaymentRequestToDatabase(Payments payment)
    {
        // Assuming there's a PaymentDal that handles saving payment data
        PaymentsDal paymentDal = new PaymentsDal();
        paymentDal.SavePayment(_dbHelper, payment);
    }

    // Fetch the latest payment for a person
    private Payments GetLatestPaymentForPerson(int personId)
    {
        PaymentsDal paymentDal = new PaymentsDal();
        DataTable paymentTable = paymentDal.GetLatestPaymentByPersonId(_dbHelper, personId);

        if (paymentTable.Rows.Count > 0)
        {
            DataRow dr = paymentTable.Rows[0];
            return new Payments
            {
                ID = Convert.ToInt32(dr["ID"]),
                Person_Id = Convert.ToInt32(dr["Person_Id"]),
                Owner_Id = Convert.ToInt32(dr["Owner_Id"]),
                Payment_CreationDate =  Convert.ToDateTime(dr["Payment_CreationDate"]).Date,
                PaymentStatus = Convert.ToBoolean(dr["Payment_Status"]),
            };
        }

        return null;
    }


    #endregion


    [HttpGet]
    public IActionResult DownloadPersonData()
    {
        // Fetch data from the database using SP
        PersonDal personDal = new PersonDal();

        var personData = personDal.GetAllPersonByOwnerId(_dbHelper);

        if (personData.Rows.Count > 0)
        {
            // Remove the 'Bed_Id' column from the DataTable before generating Excel
            if (personData.Columns.Contains("Bed_Id")|| personData.Columns.Contains("Id")|| personData.Columns.Contains("Owner_ID"))
            {
                personData.Columns.Remove("Bed_Id");
                personData.Columns.Remove("Id");
                personData.Columns.Remove("Owner_ID");
            }

            // Set the license context
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create the Excel package
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Persons");

                // Load the DataTable into the worksheet, starting from cell A1
                worksheet.Cells["A1"].LoadFromDataTable(personData, true, TableStyles.Light1);

                // Customize the Excel (e.g., AutoFit the columns)
                worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

                // Convert the Excel package to a byte array
                var excelFile = package.GetAsByteArray();

                // Send the Excel file to the client
                return File(excelFile, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "PersonData.xlsx");
            }
        }

        return BadRequest("No data available");
    }




}





//// Calculate the payment deadline based on the person's chosen plan
//private DateTime CalculatePaymentDeadline(PaymentPerson person)
//{
//    switch (person.Payment_Cycle.ToLower())
//    {
//        case "daily":
//            return DateTime.Now.AddDays(1);

//        case "weekly":
//            return DateTime.Now.AddDays(2);

//        case "monthly":
//            return DateTime.Now.AddDays(4);

//        default:
//            throw new Exception("Invalid plan chosen by the person");
//    }
//}