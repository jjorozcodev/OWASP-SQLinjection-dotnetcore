using System;

namespace SQLinjection_exposed.Data
{
    public class Employee
    {
        public int BusinessEntityID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; }
    }
}
