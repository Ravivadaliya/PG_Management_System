using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.User.Data;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.User.Controllers;

[ClientCkeckAccess]
[Area("User")]
[Route("User/[Controller]/[Action]")]
public class User(DatabaseHelper dbHelper) : Controller
{

    private readonly DatabaseHelper _dbHelper = dbHelper;
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult UserDashBoard()
    {
        PersonDal personDal = new PersonDal();
        DataTable dataTable = personDal.GetPersonAllDetailsById(_dbHelper, Convert.ToInt32(CV.Person_Id()));
        return View("UserDashBoard", dataTable);
    }

    public IActionResult IssueForm()
    {
        int user_id = Convert.ToInt32(CV.Person_Id());


        return View();
    }
    public IActionResult UserIssueList(int Room_ID)
    {
        UserIssueDal userIssueDal = new UserIssueDal();
        DataTable dataTable = userIssueDal.GetAllissueByRoomId(_dbHelper, Room_ID);
        return View("UserIssueList",dataTable);
    }

    //public IActionResult SaveIssue()
    //{

    //}
}
