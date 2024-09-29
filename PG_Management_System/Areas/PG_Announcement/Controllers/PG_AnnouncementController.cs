using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Person.Data;
using DatabaseHelperLibrary;
using PG_Management_System.Areas.PG_Announcement.Models;
using PG_Management_System.Areas.PG_Announcement.Data;
namespace PG_Management_System.Areas.PG_Announcement.Controllers;

[Area("PG_Announcement")]
[Route("PG_Announcement/[controller]/[action]")]
public class PG_AnnouncementController(DatabaseHelper _dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = _dbHelper;

    public IActionResult AddAnnouncement()
    {
        PersonDal personDal = new PersonDal();
        ViewBag.HostelList = personDal.GetHostelListByOwnerID(_dbHelper);
        return View();
    }

    public IActionResult SaveAnnouncement(Announcement announcement)
    {
        AnnouncementDal announcementDal = new AnnouncementDal();
        if (announcementDal.InsertAnnouncement(_dbHelper,announcement))
        {
            TempData["Message"] = "Announcement Send SuccessFully";
            TempData["AlertType"] = "success";
        }
        return RedirectToAction("AddAnnouncement");
    }
}
