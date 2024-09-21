using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Issues.Controllers;

[Area("PG_Issues")]
[Route("PG_Issues/[controller]/[action]")]
public class PG_IssuesController : Controller
{
    private IConfiguration _configuration;
    public PG_IssuesController(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IActionResult AllIssuesList()
    {
        return View();
    }
}
