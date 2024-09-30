using DatabaseHelperLibrary;
using Microsoft.AspNetCore.Mvc;
using PG_Management_System.Areas.PG_Owner.Data;
using PG_Management_System.Areas.PG_Owner.Models;
using PG_Management_System.Areas.PG_Person.Data;
using PG_Management_System.Areas.PG_Person.Models;
using System.Data;

namespace PG_Management_System.Areas.Login.Controllers;


[Area("Login")]
[Route("Login")]
public class LoginController(DatabaseHelper dbHelper) : Controller
{
	private readonly DatabaseHelper _dbHelper = dbHelper;
	[HttpGet("Login")]
	public IActionResult Login()
	{
		return View();
	}

	[HttpPost]
	public IActionResult LoginAction(Owner owner)
	{
		string error = null;
		if (owner.Owner_EmailId == null && owner.Owner_PassWord == null)
		{
			error += "Please Enter Email or Password";
			return RedirectToAction("Login");
		}
		if (owner.Owner_EmailId == null)
		{
			error += "Please Enter Email or Password";
		}
		if (owner.Owner_PassWord == null)
		{
			error += "Please Enter Your Password";
		}
		if (error != null)
		{
			TempData["Error"] = error;
			return RedirectToAction("Login");
		}
		else
		{
			if (owner.LoginPerson == "User")
			{
				PersonDal personDal = new PersonDal();
				Person person = new Person();

				DataTable dataTable = personDal.SelectPersonByEmailPassword(_dbHelper, owner.Owner_EmailId, owner.Owner_PassWord);

				if (dataTable.Rows.Count > 0)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						HttpContext.Session.SetString("UserId", dr["Id"].ToString());
						HttpContext.Session.SetString("Person_Name", dr["Person_Name"].ToString());
						HttpContext.Session.SetString("Person_PassWord", dr["Person_PassWord"].ToString());
						HttpContext.Session.SetString("Person_Email_Id", dr["Person_Email_Id"].ToString());
						break;
					}
				}
				else
				{
					TempData["Error"] = "Email or Password Invalid";
					return RedirectToAction("Login");
				}

				if (HttpContext.Session.GetString("Person_Email_Id") != null && HttpContext.Session.GetString("Person_PassWord") != null)
				{
					return RedirectToAction("UserDashboard", "User", new { Area = "User" });
				}
				return RedirectToAction("Login");
			}
			else
			{
				Owner owner1 = new Owner();
				OwnerDal ownerDal = new OwnerDal();
				DataTable dataTable = ownerDal.SelectOwnerByEmailPass(_dbHelper, owner.Owner_EmailId, owner.Owner_PassWord);

				if (dataTable.Rows.Count > 0)
				{
					foreach (DataRow dr in dataTable.Rows)
					{
						HttpContext.Session.SetString("AdminId", dr["Id"].ToString());
						HttpContext.Session.SetString("Owner_Name", dr["Owner_Name"].ToString());
						HttpContext.Session.SetString("Owner_PassWord", dr["Owner_PassWord"].ToString());
						HttpContext.Session.SetString("Owner_EmailId", dr["Owner_EmailId"].ToString());
						break;
					}
				}
				else
				{
					TempData["Error"] = "Email or Password Invalid";
					return RedirectToAction("Login");
				}

				if (HttpContext.Session.GetString("Owner_EmailId") != null && HttpContext.Session.GetString("Owner_PassWord") != null)
				{
					return RedirectToAction("Dashboard", "PG_Owner", new { Area = "PG_Owner" });
				}
				return RedirectToAction("Login");
			}
		}
	}

	[HttpGet("Logout")]
	public IActionResult Logout()
	{
		HttpContext.Session.Clear();
		return RedirectToAction("Login", "Login", new { Area = "Login" });
	}

}
