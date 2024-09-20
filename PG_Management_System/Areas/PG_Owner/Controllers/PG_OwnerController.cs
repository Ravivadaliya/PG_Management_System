using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas.PG_Owner.Controllers;


[Area("PG_Owner")]
[Route("PG_Owner/[controller]/[action]")]
public class PG_OwnerController : Controller
{
    private IConfiguration configuration;

    public PG_OwnerController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    public IActionResult Dashboard()
    {
        return View();
    }

    public IActionResult AdminRegistration()
    {
        return View();
    }
    public IActionResult AdminLogin()
    {
        return View();
    }
}
