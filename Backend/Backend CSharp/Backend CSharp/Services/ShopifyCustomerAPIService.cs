using Backend_CSharp.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Backend_CSharp.Services
{
    public class ShopifyCustomerAPIService
    {
        private HttpClient m_client;

        public ShopifyCustomerAPIService()
        {
            m_client = new HttpClient();
        }

        public async Task<CustomerValidationResponse> GetCustomers(int page)
        {
            Debug.WriteLine($"Beginning {nameof(GetCustomers)} - Page {page}");
            Uri uri = new Uri($"https://backend-challenge-winter-2017.herokuapp.com/customers.json?page={page}", UriKind.Absolute);
            HttpRequestMessage requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            Debug.WriteLine("Sending request ...");
            HttpResponseMessage response = await m_client.SendAsync(requestMessage);
            Debug.WriteLine("Request recieved");

            // If there was an error, return early with the error
            if (!response.IsSuccessStatusCode)
            {
                Debug.WriteLine("The response status code is not valid");
                return null;
            }

            string responseAsString = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(responseAsString);
            //Debug.WriteLine(jsonObject.ToString());
            
            // Parse Validations
            //Debug.WriteLine(jsonObject["validations"].ToString());
            var validationResults = JsonConvert.DeserializeObject<List<JObject>>(jsonObject["validations"].ToString());
            List<Validation> validations = new List<Validation>();
            foreach (var item in validationResults)
            {
                foreach (var property in item)
                {
                    // Parse the Validation Constraints
                    //Debug.WriteLine(property.Value);
                    ValidationConstraint constraint = new ValidationConstraint();

                    // Get the required if it exists
                    var requiredToken = property.Value.SelectToken("required");
                    if (requiredToken != null)
                    {
                        if (bool.TryParse(requiredToken.ToString(), out bool b))
                            constraint.Required = b;
                    }

                    // Get the Type if it exists
                    var typeToken = property.Value.SelectToken("type");
                    if (typeToken != null)
                        constraint.Type = typeToken.ToString();

                    // Get the length if it exists
                    var lengthToken = property.Value.SelectToken("length");
                    if (lengthToken != null)
                    {
                        // Assume to be string
                        constraint.Type = "string";

                        // Get the Min Token
                        var minToken = lengthToken.SelectToken("min");
                        if (minToken != null)
                        {
                            if (int.TryParse(minToken.ToString(), out int min))
                            {
                                constraint.Length.Min = min;
                            }
                        }

                        // Get the Max Token
                        var maxToken = lengthToken.SelectToken("max");
                        if (maxToken != null)
                        {
                            if (int.TryParse(maxToken.ToString(), out int max))
                            {
                                constraint.Length.Max = max;
                            }
                        }
                    }

                    // Add the Validation to the list
                    validations.Add(new Validation(property.Key, constraint));
                }
            }

            // Parse Customers
            //Debug.WriteLine(jsonObject["customers"].ToString());
            var customers = JsonConvert.DeserializeObject<List<Customer>>(jsonObject["customers"].ToString());

            // Parse Paginations
            //Debug.WriteLine(jsonObject["pagination"].ToString());
            var paginationResults = JsonConvert.DeserializeObject<JObject>(jsonObject["pagination"].ToString());
            Pagination pagination = new Pagination(paginationResults["current_page"].Value<int>(), paginationResults["per_page"].Value<int>(), paginationResults["total"].Value<int>());
            //Debug.WriteLine(pagination.ToString());

            CustomerValidationResponse returnedResponse = new CustomerValidationResponse();
            returnedResponse.Validations = validations;
            returnedResponse.Customers = customers;
            returnedResponse.Pagination = pagination;
            return returnedResponse;
        }

    }
}
