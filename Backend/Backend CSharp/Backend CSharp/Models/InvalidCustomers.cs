using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Models
{
    public class InvalidCustomer
    {
        public int id { get; set; }
        public List<string> invalid_fields { get; set; } = new List<string>();

        public InvalidCustomer(int _id)
        {
            id = _id;
        }

        public override string ToString()
        {
            return $"{nameof(id)}:{id} | {nameof(invalid_fields)}:{string.Join(", ", invalid_fields)}";
        }
    }
}
