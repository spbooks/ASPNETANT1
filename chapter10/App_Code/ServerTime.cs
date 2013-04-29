using System;
using System.Web.Services;
using System.Web.Script.Services;

[ScriptService]
[WebService(Namespace = "http://sitepoint.com/books/aspnetant1/getservertime")]
public class ServerTime : WebService
{
    [WebMethod]    
    public string GetServerTime()
    {
        return DateTime.Now.ToLongTimeString();
    }

}

