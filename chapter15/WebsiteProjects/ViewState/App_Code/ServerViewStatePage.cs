using System;
using System.Web.UI;
using System.Configuration;
using System.IO;

public class ServerViewStatePage : System.Web.UI.Page
{

  private const string _configKey = "ServerViewStateMode";
  private const string _formField = "__SERVERVIEWSTATEKEY";

  private string ViewStateData
  {
    get { return Request.Form[_formField]; }
    set { ClientScript.RegisterHiddenField(_formField, value); }
  }

  private string PersistenceType
  {
    get { return (ConfigurationManager.AppSettings[_configKey] ?? "").ToLower(); }
  }
	
  private object ToObject(string viewstate)
  {
    byte[] b = Convert.FromBase64String(viewstate);
    LosFormatter lf = new LosFormatter();
    return lf.Deserialize(Convert.ToBase64String(b));
  }

  private string ToBase64String(object state)
  {
    LosFormatter lf = new LosFormatter();
    StringWriter sw = new StringWriter();
    lf.Serialize(sw, state);
    byte[] b = Convert.FromBase64String(sw.ToString());
    return Convert.ToBase64String(b);
  }

  private string ToSession(string value)
  {
    string key = Guid.NewGuid().ToString();
    Session.Add(key, value);
    return key;
  }

  private string FromSession(string key)
  {
    string value = Convert.ToString(Session[key]);
    Session.Remove(key);
    return value;
  }

  private string ToDb(string value)
  {
    //You will need to write the code to save the value to the database
    //and return a unique key.
    return "unique db key";
  }

  private string FromDb(string key)
  {
    //You will need to write the code which looks up the viewstate string
    //by the unique key.
    return "viewstate string from db";
  }

  protected override object LoadPageStateFromPersistenceMedium()
  {
    switch (PersistenceType)
    {
      case "session":
        return ToObject(FromSession(ViewStateData));
      case "database":
        return ToObject(FromDb(ViewStateData));
      default:
        return base.LoadPageStateFromPersistenceMedium();
    }
  }

  protected override void SavePageStateToPersistenceMedium(object ViewStateObject)
  {
    switch (PersistenceType)
    {
      case "session":
        ViewStateData = ToSession(ToBase64String(ViewStateObject));
        break;
      case "database":
        ViewStateData = ToDb(ToBase64String(ViewStateObject));
        break;
      default:
        base.SavePageStateToPersistenceMedium(ViewStateObject);
        break;
    }
  }

}
