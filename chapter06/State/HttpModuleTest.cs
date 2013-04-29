using System;
using System.Web;

namespace State
{
  public class SomeHttpModule : IHttpModule
  {
///<summary>
///Initializes a module and prepares it to handle requests.
///</summary>
///
///<param name="context">An <see cref="T:System.Web.HttpApplication"></see> that provides access to the methods, properties, and events common to all application objects within an ASP.NET application </param>
public void Init(HttpApplication context)
{
  context.AcquireRequestState += new EventHandler(context_AcquireRequestState);
}

void context_AcquireRequestState(object sender, EventArgs e)
{
  HttpContext.Current.Items["AcquireRequestState"] = DateTime.Now;
}

    ///<summary>
    ///Disposes of the resources (other than memory) used by the module that implements <see cref="T:System.Web.IHttpModule"></see>.
    ///</summary>
    ///
    public void Dispose()
    {
      throw new NotImplementedException();
    }
  }
}
