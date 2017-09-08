using Backend_CSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp
{
    public class Program
    {
        public static ShopifyCustomerAPIService m_CustomerAPIService = new ShopifyCustomerAPIService();

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
            var apiCall = await m_CustomerAPIService.GetCustomers(input);

            Console.WriteLine(apiCall);
        }

    }
}
