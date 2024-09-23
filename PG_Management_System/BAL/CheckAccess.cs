using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
namespace PG_Management_System.BAL;

public class CheckAccess : ActionFilterAttribute, IAuthorizationFilter
{
	public void OnAuthorization(AuthorizationFilterContext filterContext)
	{
		if (filterContext.HttpContext.Session.GetString("Id") == null)
		{
			filterContext.Result = new RedirectResult("~/PG_Owner/PG_Owner/AdminLogin");
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
