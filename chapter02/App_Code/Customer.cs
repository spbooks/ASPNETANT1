using System;
using System.Collections.Generic;
using System.Text;

namespace chapter_02_core_libraries
{
    public class Customer : Person
    {
        public int CustomerID;

        public Customer() { }

        public Customer(int customerID)
        {
            this.CustomerID = customerID;
        }

        public Customer(int customerID, Address address) : this(customerID)
        {
            this.Address = address;
        }
    }
}
