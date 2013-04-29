using System;
using System.Collections.Generic;
using System.Text;

namespace chapter_02_core_libraries
{
    public class Employee : Person
    {
        public int EmployeeID; 
        public bool IsManager;

        public Employee() { }

        public Employee(int employeeID, bool isManager)
        {
            this.EmployeeID = employeeID;
            this.IsManager = isManager;
        }

        public Employee(int employeeID, bool isManager, Address address)
            : this(employeeID, isManager)
        {
            this.Address = address;
        }
    }
}
