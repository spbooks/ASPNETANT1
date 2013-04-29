using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

/// <summary>
/// Person test class for lists
/// </summary>
public class Person
{
    public Person()
    { }
    private Guid id=Guid.NewGuid();

    public Guid Id
    {
        get { return id; }
        set { id = value; }
    }


    private string firstName;

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }


    private string lastName;

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    private string blogName;

    public string BlogName
    {
        get { return blogName; }
        set { blogName = value; }
    }
    
    private string url;

    public string Url
    {
        get { return url; }
        set { url = value; }
    }

    public static List<Person> GetPersons()
    {
        if (persons == null)
        {
            persons = new List<Person>();
            Person p;
            p = new Person();
            p.FirstName = "Phil";
            p.LastName = "Haack";
            p.BlogName = "You've Been Haacked";
            p.Url = "http://www.haacked.com/";
            persons.Add(p);
            p = new Person();
            p.FirstName = "Jeff";
            p.LastName = "Atwood";
            p.BlogName = "Coding Horror";
            p.Url = "http://www.codinghorror.com/blog";
            persons.Add(p);
            p = new Person();
            p.FirstName = "Scott";
            p.LastName = "Allen";
            p.BlogName = "Ode to Code";
            p.Url = "http://www.OdeToCode.com/blogs/scott";
            persons.Add(p);
            p = new Person();
            p.FirstName = "Jon";
            p.LastName = "Galloway";
            p.BlogName = "Jon's Blog";
            p.Url = "http://weblogs.asp.net/jgalloway/";
            persons.Add(p);
        }
        return persons;
    }

    private static List<Person> persons;
}
