using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity.Entities
{
    public class Student
    {
        public int id { get; set; }

        public string name { get; set; }

        public Address address { get; set; }
    }
}
