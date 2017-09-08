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
        public ValidationConstraint Constraint { get; set; }

        public Validation(string name, ValidationConstraint constraint)
        {
            Name = name;
            Constraint = constraint;
        }
    }

    public class ValidationConstraint
    {
        public bool Required { get; set; } = false;
        public string Type { get; set; } = "";
        public Length Length { get; set; } = new Length();
    }

    public class Length
    {
        public int? Min { get; set; } = null;
        public int? Max { get; set; } = null;

        public bool Exists { get { return Min != null || Max != null; } }

        public Length() { }
        public Length(int? min, int? max)
        {
            Min = min;
            Max = max;
        }

        public bool IsValid(string s)
        {
            // Check early exit
            if (Min == null && Max == null) return false;

            bool valid = true;

            // Only allow strings that are Greater Than or Equal to the Minimum
            if (Min.HasValue)
            {
                if (s.Length < Min.Value)
                    valid = false;
            }
            // Only allow strings that are Less Than or Equal to the Maximum
            if (Max.HasValue)
            {
                if (s.Length > Max.Value)
                    valid = false;
            }

            return valid;
        }
    }
}
