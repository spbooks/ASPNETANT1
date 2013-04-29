using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace chapter_02_core_libraries
{
    public partial class Predicates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Sorting<br />");
            SortingDemonstration();

            Response.Write("<br />");
            Response.Write("Predicates<br />");
            PredicateDemonstration();

            Response.Write("<br />");
            Response.Write("Date Conversion<br />");
            DateConversionSample();
        }

        #region "Sorting"
        private void SortingDemonstration()
        {
            string[] names = { "Bob", "Sue", "Jim", "Edgar" };
            int[] values = { 456, 234, 567, 123, 890 };
            DateTime[] dates = { new DateTime(1950,2,3), 
                                new DateTime(1970,4,5),
                                new DateTime(2000,1,1) };
            WriteSortedValues(new List<string>(names));
            WriteSortedValues(new List<int>(values));
            WriteSortedValues(new List<DateTime>(dates));
        }

        void WriteSortedValues<T>(List<T> list)
        {
            list.Sort();
            list.ForEach(
                delegate(T item) { Response.Write(item + "<br />"); }
            );
        }
        #endregion

        #region "Predicate"
        public void PredicateDemonstration()
        {
            Response.Write("California Employees:<br />");
            GetCaliforniaEmployees().ForEach(
                    delegate(Employee emp)
                    { Response.Write(string.Format("Employee #{0} lives at {1} in {2}<br />", 
                        emp.EmployeeID, 
                        emp.Address.Street, 
                        emp.Address.Region)); }
                );
            Response.Write("Non-California Customers:<br />");
            GetNonCaliforniaCustomers().ForEach(
                    delegate(Customer cust)
                    { Response.Write(string.Format("Customer #{0} lives at {1} in {2}<br />", 
                        cust.CustomerID, 
                        cust.Address.Street, 
                        cust.Address.Region)); }
                );
        }

        public bool IsCalifornian(Person p)
        {
            return (p.Address.Region == "California");
        }

        public List<Employee> GetCaliforniaEmployees()
        {
            List<Employee> employees = GetEmployees();
            return employees.FindAll(IsCalifornian);
        }

        public List<Customer> GetNonCaliforniaCustomers()
        {
            List<Customer> customers = GetCustomers();
            customers.RemoveAll(IsCalifornian);
            return customers;
        }

        public List<Employee> GetEmployees()
        {
            int i = 1;
            return new List<Employee>(
                new Employee[]{
                new Employee(i++,false,GetRandomAddress()),
                new Employee(i++,true,GetRandomAddress()),
                new Employee(i++,false,GetRandomAddress()),
                new Employee(i++,true,GetRandomAddress()),
                new Employee(i++,false,GetRandomAddress()),
                new Employee(i++,true,GetRandomAddress()),
                new Employee(i++,false,GetRandomAddress()),
                new Employee(i++,true,GetRandomAddress()),
                new Employee(i++,false,GetRandomAddress()),
                new Employee(i++,true,GetRandomAddress()),
                }
            );
        }

        public List<Customer> GetCustomers()
        {
            int i = 1;
            return new List<Customer>(
                new Customer[]{
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                new Customer(i++,GetRandomAddress()),
                }
            );
        }

        public List<Employee> GetManagers()
        {
            List<Employee> employeeList = GetEmployees();
            return employeeList.FindAll(
                delegate(Employee emp)
                { return emp.IsManager == true; }
            );
        }

        public Employee Get(int id)
        {
            List<Employee> employeeList = GetEmployees();
            return employeeList.Find(
                delegate(Employee emp)
                { return emp.EmployeeID == id; }
            );
        }

        string[] streets = "Main,First,Second,Cedar,Elm,Grand,Juniper,Date,Fir,State".Split(',');
        string[] states = "California,California,California,California,California,Washington,New York,Florida,Oregon,Ohio".Split(',');
        //string[]
        public Address GetRandomAddress()
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());
            Address address = new Address();
            address.Street = string.Format("{0} {1} Street", rand.Next(1000), streets[rand.Next(10)]);
            address.Region = states[rand.Next(10)];
            address.City = "Springfield"; //There's one in just about every U.S. State.
            address.PostalCode = string.Format("{0:00000}", rand.Next(99999));
            return address;
        }
        #endregion

        #region "Date Conversion"
        public void DateConversionSample()
        {
            List<DateTime> dates = new List<DateTime>();
            for (DateTime d = DateTime.Now;
                 d < DateTime.Now.AddMonths(10);
                 d = d.AddDays(2))
            { dates.Add(d); }

            //Convert date list to short date (string) list
            List<string> strings = dates.ConvertAll<string>(
                delegate(DateTime value)
                { return value.ToShortDateString(); }
               );
            Response.Write(strings[0] + "<br />");

            //Convert date list to day of year (int) list
            List<int> ints = dates.ConvertAll<int>(
                delegate(DateTime value)
                { return value.DayOfYear; }
               );
            Response.Write(ints[0] + "<br />");

            //Convert date list to dayligt savings time (bool) list
            List<bool> bools = dates.ConvertAll<bool>(
                delegate(DateTime value)
                { return value.IsDaylightSavingTime(); }
               );
            Response.Write(bools[0] + "<br />");

            //List<string> strings = dates.ConvertAll<string>
            //   (new Converter<DateTime, string>(
            //   delegate(DateTime d) 
            //   { return d.ToShortDateString(); }));
        }
        #endregion
    }
}