using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Models
{
    public class CustomerValidationResponse
    {
        public List<Validation> Validations { get; set; }
        public List<Customer> Customers { get; set; }
        public Pagination Pagination { get; set; }
    }
}
