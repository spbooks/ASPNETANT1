using System;
using System.Collections.Generic;
using System.Text;

namespace chapter_02_core_libraries
{
    public struct Address
    {
        public string Street;
        public string City;
        public string Region;
        public string PostalCode;

        public Address(string street, string city, string region, string postalCode)
        {
            this.Street = street;
            this.City = city;
            this.Region = region;
            this.PostalCode = postalCode;
        }
    }
}
