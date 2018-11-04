using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RESTfulAPIDesign.Models
{
    public class Person
    {
        public long Id { get; set; }
        public string FirstName { get; set; }
        public long SecondName { get; set; }
        public long Address { get; set; }
        public long Gender { get; set; }
    }
}
