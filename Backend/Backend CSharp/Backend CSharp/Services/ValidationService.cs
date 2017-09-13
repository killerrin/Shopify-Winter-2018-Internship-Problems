using Backend_CSharp.Models;
using Backend_CSharp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Backend_CSharp.Services
{
    public class ValidationService
    {
        public List<InvalidCustomer> PreformValidations(IReadOnlyList<Validation> validations, IReadOnlyList<Customer> customers)
        {
            Dictionary<int, InvalidCustomer> invalidCustomers = new Dictionary<int, InvalidCustomer>();
            foreach (var validation in validations)
            {
                Debug.WriteLine($"Checking Validation: {validation.Name}");
                foreach (var customer in customers)
                {
                    object propValue = customer.GetPropValue(validation.Name);

                    // Check for Required
                    if (validation.Constraint.Required)
                    {
                        if (propValue == null)
                        {
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "Required");
                        }
                    }

                    // Check for Type
                    if (string.IsNullOrWhiteSpace(validation.Constraint.Type)) { } // If empty or not specified. Allow everything
                    else if (validation.Constraint.Type == "string")
                    {
                        if (!(propValue is string))
                        {
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "TypeCheckString");
                        }
                    }
                    else if (validation.Constraint.Type == "boolean")
                    {
                        if (!(propValue is bool))
                        {
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "TypeCheckBoolean");
                        }
                    }
                    else if (validation.Constraint.Type == "number")
                    {
                        if (!validation.Constraint.Required && propValue == null) { }
                        else if (!validation.Constraint.Required && (propValue as int?) == -1) {
                            // The provided value was a string. A string hack was applied to determine the difference between nulls and invalid strings
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "TypeCheckNumber(StringHack)");
                        }
                        else if (propValue is int || propValue is double || propValue is float) { }
                        else if (propValue is int? || propValue is double? || propValue is float?) { }
                        else
                        {
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "TypeCheckNumber");
                        }
                    }

                    // Check for Length
                    if (validation.Constraint.Length.Exists)
                    {
                        if (!validation.Constraint.Length.IsValid((string)propValue))
                        {
                            FlagError(ref invalidCustomers, customer, validation.Name, propValue, "LengthCheck");
                        }
                    }

                    //if (propValue == null) continue;
                    //Debug.WriteLine($"{propValue} | {propValue.GetType()}");
                }

            }

            Debug.WriteLine($"{nameof(PreformValidations)} - Completed ");
            return invalidCustomers.Values.ToList();
        }

        private void FlagError(ref Dictionary<int, InvalidCustomer> invalidCustomers, Customer c, string invalidPropertyName, object value, string where)
        {
            Debug.WriteLine($"Error Detected --- Customer:{c.id} | Property Name:{invalidPropertyName} | Value:{value} | Where:{where}");

            // If it doesn't exist, create it
            if (!invalidCustomers.ContainsKey(c.id))
                invalidCustomers[c.id] = new InvalidCustomer(c.id);

            // Cache the invalid property
            var invalidCustomer = invalidCustomers[c.id];

            // Is the invalid property name already in the list? If so exit
            if (invalidCustomer.invalid_fields.Where(x => x == invalidPropertyName).FirstOrDefault() != null)
                return;

            // If not, add it
            invalidCustomer.invalid_fields.Add(invalidPropertyName);
        }
    }
}
