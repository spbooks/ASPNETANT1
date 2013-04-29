using System;
using System.Collections.Generic;
using System.Text;

namespace chapter_02_core_libraries
{
    //Console application version of this sample.
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Sorting");
            SortingDemonstration();

            Console.WriteLine();
            Console.WriteLine("Predicates");
            PredicateDemonstration();

            Console.WriteLine();
            Console.WriteLine("Date Conversion");
            DateConversionSample();

            Console.WriteLine("Press Enter to close...");
            Console.ReadLine();
        }

        #region "Sorting"
        private static void SortingDemonstration()
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

        static void WriteSortedValues<T>(List<T> list)
        {
            list.Sort();
            list.ForEach(
                delegate(T item) { Console.WriteLine(item); }
            );
        }
        #endregion

        #region "Predicate"
        public static void PredicateDemonstration()
        {
            Console.WriteLine("California Employees:");
            GetCaliforniaEmployees().ForEach(
                    delegate(Employee emp)
                    { Console.WriteLine(string.Format("Employee #{0} lives at {1} in {2}", emp.EmployeeID, emp.Address.Street, emp.Address.Region)); }
                );
            Console.WriteLine("Non-California Customers:");
            GetNonCaliforniaCustomers().ForEach(
                    delegate(Customer cust)
                    { Console.WriteLine(string.Format("Customer #{0} lives at {1} in {2}", cust.CustomerID, cust.Address.Street, cust.Address.Region)); }
                );
        }

        public static bool IsCalifornian(Person p)
        {
            return (p.Address.Region == "California");
        }

        public static List<Employee> GetCaliforniaEmployees()
        {
            List<Employee> employees = GetEmployees();
            return employees.FindAll(IsCalifornian);
        }

        public static List<Customer> GetNonCaliforniaCustomers()
        {
            List<Customer> customers = GetCustomers();
            customers.RemoveAll(IsCalifornian);
            return customers;
        }

        public static List<Employee> GetEmployees()
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

        public static List<Customer> GetCustomers()
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

        public static List<Employee> GetManagers()
        {
            List<Employee> employeeList = GetEmployees();
            return employeeList.FindAll(
                delegate(Employee emp)
                { return emp.IsManager == true; }
            );
        }

        public static Employee Get(int id)
        {
            List<Employee> employeeList = GetEmployees();
            return employeeList.Find(
                delegate(Employee emp)
                { return emp.EmployeeID == id; }
            );
        }

        static string[] streets = "Main,First,Second,Cedar,Elm,Grand,Juniper,Date,Fir,State".Split(',');
        static string[] states = "California,California,California,California,California,Washington,New York,Florida,Oregon,Ohio".Split(',');
        //string[]
        public static Address GetRandomAddress()
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
        public static void DateConversionSample()
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
            Console.WriteLine(strings[0]);

            //Convert date list to day of year (int) list
            List<int> ints = dates.ConvertAll<int>(
                delegate(DateTime value)
                { return value.DayOfYear; }
               );
            Console.WriteLine(ints[0]);

            //Convert date list to dayligt savings time (bool) list
            List<bool> bools = dates.ConvertAll<bool>(
                delegate(DateTime value)
                { return value.IsDaylightSavingTime(); }
               );
            Console.WriteLine(bools[0]);

            //List<string> strings = dates.ConvertAll<string>
            //   (new Converter<DateTime, string>(
            //   delegate(DateTime d) 
            //   { return d.ToShortDateString(); }));
        }
        #endregion
    }
}