using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;

namespace PG_Management_System.Areas;

public class BaseController : Controller
{
    protected readonly DatabaseHelper _dbHelper;

    public BaseController(DatabaseHelper dbHelper)
    {
        _dbHelper = dbHelper;
        _dbHelper.OpenConnection(); // Open connection for every controller that inherits from this
    }

    protected override void Dispose(bool disposing)
    {
        _dbHelper.CloseConnection(); // Close connection when controller is disposed
        base.Dispose(disposing);
    }
}
