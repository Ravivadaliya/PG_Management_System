using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace PG_Management_System.BAL;

public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.Session.GetString("AdminId") == null)
		{
			filterContext.Result = new RedirectResult("~/Login/Login/Login");
		}
	}
	public override void OnResultExecuting(ResultExecutingContext filterContext)
	{
		filterContext.HttpContext.Response.Headers["Cache-Control"] = "no-cache, no-store, must-revalidate";
		filterContext.HttpContext.Response.Headers["Expires"] = "-1";
		filterContext.HttpContext.Response.Headers["Pragma"] = "no-cache";
		base.OnResultExecuting(filterContext);
	}
}
