using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderViewer_UWP.Models.Shopify;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OrderViewer_UWP.Services
{
    public class ShopifyOrdersAPIService
    {
        private HttpClient m_client;

        public ShopifyOrdersAPIService()
        {
            m_client = new HttpClient();
        }

        public async Task<RootObject> GetAllOrders()
        {
            Debug.WriteLine($"Beginning {nameof(GetAllOrders)}");
            Uri uri = new Uri($"https://shopicruit.myshopify.com/admin/orders.json?page=1&access_token=c32313df0d0ef512ca64d5b336a0d7c6", UriKind.Absolute);
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
            var deserializedResponse = JsonConvert.DeserializeObject<RootObject>(jsonObject.ToString());
            return deserializedResponse;
        }
    }
}
