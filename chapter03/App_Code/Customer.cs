using System;
using System.Web;
using System.Data;
using System.Collections.Generic;

[Serializable]
public class Customer
{
    //Must be properties, not fields
    //Error: �The data source for GridView with id 'GridView1' did not have any 
    //properties or attributes from which to generate columns.
    //Ensure that your data source has content.� 
    //http://unboxedsolutions.com/sean/archive/2005/01/22/428.aspx
    private int customerID;
    public int CustomerID
    {
        get { return customerID; }
        set { customerID = value; }
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

    private string address;
    public string Address
    {
        get { return address; }
        set { address = value; }
    }

    private string city;
    public string City
    {
        get { return city; }
        set { city = value; }
    }

    private string state;
    public string State
    {
        get { return state; }
        set { state = value; }
    }

    public Customer()
    {
    }

	public Customer(int customerID, 
        string firstName, 
        string lastName, 
        string address, 
        string city, 
        string state)
	{
        this.CustomerID = customerID;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Address = address;
        this.City = city;
        this.State = state;
    }
}

public class CustomerData
{
    public CustomerData()
    {
        if (Customers.Rows.Count == 0)
        {
            FetchCustomers();
        }
    }

    public void Update(int customerID,
        string firstName,
        string lastName,
        string address,
        string city,
        string state)
    {
        Customer c = Get(customerID);

        c.CustomerID = customerID;
        c.FirstName = firstName;
        c.LastName = lastName;
        c.Address = address;
        c.City = city;
        c.State = state;
    }

    public IEnumerable<Customer> GetCustomers()
    {
        foreach (DataRow row in Customers.Rows)
            yield return CustomerFromRow(row);
    }

    //public IEnumerable<Customer> GetCustomers(int rows, int startIndex)
    //{
    //    if (rows == 0) rows = Customers.Rows.Count;
    //    List<Customer> pageCustomers = new List<Customer>();
    //    for (int i = startIndex; i <= rows && i <= Customers.Rows.Count - 1; i++)
    //        yield return CustomerFromRow(Customers.Rows[i]);
    //}

    //Sorting is only supported for ObjectDataSources whose Select method returns
    //DataTable, DataView, or DataSet. To add sorting to IEnumerable returns,
    //need to override GridView sorting, ObjectDataSource select, and class IComparer
    //http://www.codeproject.com/useritems/GridViewObjectDataSource.asp
    public List<Customer> GetCustomers(int rows, int startIndex)
    {
        if (rows == 0) rows = Customers.Rows.Count;
        List<Customer> pageCustomers = new List<Customer>();
        for (int i = startIndex; i <= rows && i <= Customers.Rows.Count - 1; i++)
            pageCustomers.Add(CustomerFromRow(Customers.Rows[i]));
        return pageCustomers;
    }

    public Customer Get(int id)
    {
        return FetchCustomerById(id);
    }

    public void Add(Customer c)
    {
        Customers.Rows.Add(
            c.CustomerID,
            c.FirstName,
            c.LastName,
            c.Address,
            c.City,
            c.State
            );
    }

    public void Delete(int id)
    {
        DataRow[] rows = Customers.Select("CustomerID = " + id);
        if (rows.Length == 1)
            Customers.Rows.Remove(rows[0]);
    }

    public void Delete(Customer c)
    {
        Delete(c.CustomerID);
    }

    public int Count()
    {
        return Customers.Rows.Count;
    }

    // For simplicity, we're reading from internal DataTable.
    // The following methods populate and manipulate our test data.
    // These methods could be working against any data source,
    // including webservices, files, etc.

    private void FetchCustomers()
    {
        string[] First = new string[] { 
                "Bob", "Phil", "Edna", "Sue", "George" };
        string[] Last = new string[] { 
                "Smith", "Johnson", "Williams", "Jones", "Brown" };

        Random rng = new Random(Guid.NewGuid().GetHashCode());
        for (int i = 1; i < 50; i++)
            this.Add(
                new Customer(
                i, First[rng.Next(5)],
                Last[rng.Next(5)],
                rng.Next(1000) + " Main St.",
                "Dallas",
                "TX"));
    }

    private Customer FetchCustomerById(int id)
    {
        DataRow[] rows = Customers.Select("CustomerID = " + id);
        if (rows.Length == 1)
        {
            return CustomerFromRow(rows[0]);
        }
        return null;
    }

    private Customer CustomerFromRow(DataRow row)
    {
        Customer c = new Customer(
            int.Parse(row["CustomerID"].ToString()),
            row["FirstName"].ToString(),
            row["LastName"].ToString(),
            row["Address"].ToString(),
            row["City"].ToString(),
            row["State"].ToString()
            );
        return c;
    }

    private DataTable Customers
    {
        get
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            DataTable dt = context.Session["CustomerData"] as DataTable;
            if (context.Session["CustomerData"] as DataTable == null)
            {
                context.Session["CustomerData"] = CreateCustomerTable();
            }
            return context.Session["CustomerData"] as DataTable;
        }
        set
        {
            System.Web.HttpContext.
                Current.Session["CustomerData"] = value;
        }
    }

    private DataTable CreateCustomerTable()
    {
        DataTable dt = new DataTable("Customers");
        dt.Columns.Add("CustomerID", typeof(Int32));
        dt.Columns.Add("FirstName", typeof(string));
        dt.Columns.Add("LastName", typeof(string));
        dt.Columns.Add("Address", typeof(string));
        dt.Columns.Add("City", typeof(string));
        dt.Columns.Add("State", typeof(string));
        return dt;
    }
}