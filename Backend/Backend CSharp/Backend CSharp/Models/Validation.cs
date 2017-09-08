using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Models
{
    public class Validation
    {
        public string Name { get; set; }
        public ValidationConstraints Constraint { get; set; }
    }

    public class ValidationConstraints
    {
        public bool? Required { get; set; }
        public string Type { get; set; }
        public Length Length { get; set; }
    }

    public class Length
    {
        public int? Min { get; set; }
        public int? Max { get; set; }
    }
}
