using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Person.Data;
using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Announcement.Models;
using PG_Management_System.Areas.PG_Announcement.Data;

namespace PG_Management_System.Areas.PG_Announcement.Controllers;

[Area("PG_Announcement")]
[Route("Announcement")]
public class PG_AnnouncementController : Controller
{
    private readonly DatabaseHelper _dbHelper;

    public PG_AnnouncementController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection();
    }

    [HttpGet("AddAnnouncement")]
    public IActionResult AddAnnouncement()
    {
        PersonDal personDal = new PersonDal();
        ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);
        return View();
    }

    [HttpPost("SaveAnnouncement")]
    public IActionResult SaveAnnouncement(Announcement announcement)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Please correct the errors in the form.";
                TempData["AlertType"] = "error";
                return RedirectToAction("AddAnnouncement");
            }

            AnnouncementDal announcementDal = new AnnouncementDal();
            if (announcementDal.InsertAnnouncement(_dbHelper, announcement))
            {
                TempData["Message"] = "Announcement sent successfully!";
                TempData["AlertType"] = "success";
            }
            else
            {
                TempData["Message"] = "Error occurred";
                TempData["AlertType"] = "error";
            }

            return RedirectToAction("AddAnnouncement");
        }
        catch (Exception ex)
        {
            TempData["Message"] = "Error While Save Announcement";
            TempData["AlertType"] = "error";
            return RedirectToAction("AddAnnouncement");
        }
    }
}
