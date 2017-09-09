using Backend_CSharp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp
{
    public class Program
    {
        public static ShopifyCustomerAPIService CustomerAPIService = new ShopifyCustomerAPIService();
        public static ValidationService ValidationService = new ValidationService();

        static void Main(string[] args)
        {
            bool runMainLoop = true;
            while (runMainLoop)
            {
                Console.WriteLine("Which page would you like to access? ... or 'exit' to exit");
                var inputStr = Console.ReadLine();

                if (inputStr == "exit")
                {
                    runMainLoop = false;
                    break;
                }
                else if (int.TryParse(inputStr, out int input))
                {
                    CallAPI(input);
                }
            }
        }
        
        public static async void CallAPI(int input)
        {
            var apiCall = await CustomerAPIService.GetCustomers(input);
            var invalidCustomers = ValidationService.PreformValidations(apiCall.Validations, apiCall.Customers);
            foreach (var invalidCustomer in invalidCustomers)
            {
                Console.WriteLine(invalidCustomer.ToString());
                Debug.WriteLine(invalidCustomer.ToString());
            }
        }

    }
}
