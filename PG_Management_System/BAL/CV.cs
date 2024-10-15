namespace PG_Management_System.BAL;

public class CV
{
	private static IHttpContextAccessor _httpContextAccessor;
	static CV()
	{
		_httpContextAccessor = new HttpContextAccessor();
	}

	public static string? Owner_Id()
	{
		string? Owner_ID = null;	

		if (_httpContextAccessor.HttpContext.Session.GetString("AdminId") != null)
		{
			Owner_ID = _httpContextAccessor.HttpContext.Session.GetString("AdminId").ToString();
		}
		return Owner_ID;
	}

    public static string? PG_Name()
    {
        string? PG_Name = null;

        if (_httpContextAccessor.HttpContext.Session.GetString("PGName") != null)
        {
            PG_Name = _httpContextAccessor.HttpContext.Session.GetString("PGName").ToString();
        }
        return PG_Name;
    }
    public static string? Owner_EmailId()
	{
		string? Owner_EmailId = null;

		if (_httpContextAccessor.HttpContext.Session.GetString("Owner_EmailId") != null)
		{
			Owner_EmailId = _httpContextAccessor.HttpContext.Session.GetString("Owner_Email_Id").ToString();
		}
		return Owner_EmailId;
	}

	public static string? Owner_PassWord()
	{
		string? Owner_PassWord = null;

		if (_httpContextAccessor.HttpContext.Session.GetString("Owner_PassWord") != null)
		{
			Owner_PassWord = _httpContextAccessor.HttpContext.Session.GetString("Owner_PassWord").ToString();
		}
		return Owner_PassWord;
	}
	public static string? Owner_Name()
	{
		string? Owner_Name = null;

		if (_httpContextAccessor.HttpContext.Session.GetString("Owner_Name") != null)
		{
			Owner_Name = _httpContextAccessor.HttpContext.Session.GetString("Owner_Name").ToString();
		}
		return Owner_Name;
	}


	public static string? Person_Name()
	{
		string? Person_Name = null;

		if (_httpContextAccessor.HttpContext.Session.GetString("Person_Name") != null)
		{
			Person_Name = _httpContextAccessor.HttpContext.Session.GetString("Person_Name").ToString();
		}
		return Person_Name;
	}
	public static string? Person_Id()
	{
		string? UserId = null;

		if (_httpContextAccessor.HttpContext.Session.GetString("UserId") != null)
		{
			UserId = _httpContextAccessor.HttpContext.Session.GetString("UserId").ToString();
		}
		return UserId;
	}


}
