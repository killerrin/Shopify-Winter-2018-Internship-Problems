using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Models
{
    public class Customer
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Age { get; set; }
        public string Country { get; set; }
        public bool? Newsletter { get; set; }
    }
}
