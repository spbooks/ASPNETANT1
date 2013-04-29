using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;

namespace Country
{

public sealed class UserOnlineHelper
{
  public IList<string> GetOnlineUsernames()
  {
    Cache cache = HttpContext.Current.Cache;

    IList<string> usernames = cache["online-users"] as IList<string>;
    if(usernames == null)
    {
      usernames = LoadOnlineUserNames();
      cache.Insert("online-users", usernames, null, DateTime.Now.AddMinutes(1), Cache.NoSlidingExpiration);
    }
    return usernames;
  }

  IList<string> LoadOnlineUserNames()
  {
    //some code to get usernames.
	//Faking it here...
	  return new List<string>(new string[] {"Bill", "Melinda", "Steve" });
  }
}

public sealed class CountryHelper
{
  CountryHelper()
  {
      HttpContext.Current.Application["countries"] = LoadCountries();
  }

  public static IList<Country> GetCountries()
  {
    IList<Country> countries = null;
  	HttpApplicationState application = HttpContext.Current.Application;

	countries = (IList<Country>)application["countries"];

    return countries;
  }

  static IList<Country> LoadCountries()
  {
    //pretend this came from the database.
	  return new List<Country>(new Country[] { new Country("USA"), new Country("Korea"), new Country("Japan") });
  }
}

  public class Country
  {
    public Country(string name)
    {
    }
  }
}
