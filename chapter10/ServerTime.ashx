<%@ WebHandler Language="C#" Class="ServerTime" %>

using System;
using System.Web;

public class ServerTime : IHttpHandler 
{
    
    public void ProcessRequest (HttpContext context) 
    {
        context.Response.ContentType = "text/plain";
        context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        context.Response.Write(DateTime.Now.ToLongTimeString());
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}