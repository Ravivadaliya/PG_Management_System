using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Issues.Data;
using PG_Management_System.BAL;
using System.Data;

namespace PG_Management_System.Areas.PG_Issues.Controllers;

[CheckAccess]
[Area("PG_Issues")]
[Route("PG_Issues/[controller]/[action]")]
public class PG_IssuesController(DatabaseHelper dbHelper) : Controller
{
    private readonly DatabaseHelper _dbHelper = dbHelper;
    public IActionResult AllIssuesList()
    {
        IssueDal issueDal = new IssueDal();
        DataTable dataTable = issueDal.GetAllissueByOwnerId(_dbHelper);
        return View("AllIssuesList",dataTable);
    }
}
