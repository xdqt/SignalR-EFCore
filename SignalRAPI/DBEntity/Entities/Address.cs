using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace DBEntity.Entities
{
    public class Address
    {
        public int id { get; set; }

        public string  location { get; set; }

        //public ICollection<Student> stu { get; set; }
    }
}
