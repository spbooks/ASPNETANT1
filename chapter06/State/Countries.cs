using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Country
{
public static class CountryHelper
{
  private static ReadOnlyCollection<Country> _countries = GetAllCountries();

  public static ReadOnlyCollection<Country> Countries
  {
    get
    {
      return _countries;
    }
  }

  private static ReadOnlyCollection<Country> GetAllCountries()
  {
    IList<Country> countries = new List<Country>();
    countries.Add(new Country("Alabama"));
    countries.Add(new Country("Alaska"));
    //...
    countries.Add(new Country("Wyoming"));
    return new ReadOnlyCollection<Country>(countries);
  }
}

public static class CountryHelper2
{
  private static readonly IList<Country> _countries = GetAllCountries();

  public static IList<Country> Countries
  {
    get
    {
      return _countries;
    }
  }

  private static IList<Country> GetAllCountries()
  {
    IList<Country> countries = new List<Country>();
    countries.Add(new Country("Alabama"));
    countries.Add(new Country("Alaska"));
    //...
    countries.Add(new Country("Wyoming"));
    return countries;

	  CountryHelper2.Countries.Add(new Country("Confusion"));
  }
}

  public class Country
  {
    public Country(string name)
    {
    }
  }
}
