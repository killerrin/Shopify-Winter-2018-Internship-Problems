using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Models
{
    public class Customer
    {
        public int id { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public int? age { get; set; }
        public string country { get; set; }
        public bool? newsletter { get; set; }
    }
}
