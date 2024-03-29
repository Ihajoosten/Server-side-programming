﻿using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace Client.Extentsions.Dish
{
    public static class MenuMethods
    {
        // Now define your asynchronous method which will retrieve all your pokemon.
        public static async Task<JArray> GetMenus()
        {
            // Define your baseUrl
            string baseUrl = "https://easy-meal-sswp-api.azurewebsites.net/api/Dish";

            // Have your using statements within a try/catch blokc that will catch any exceptions.
            try
            {
                // We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    // The HttpResponseMessage which contains status code, and data from response.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        // Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            // Return the array
                            var data = await content.ReadAsStringAsync();
                            return JArray.Parse(data);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }

        public static async Task<JObject> GetMenuById(this int id)
        {
            // Define your baseUrl with index
            string baseUrl = $"https://easy-meal-sswp-api.azurewebsites.net/api/Menu/{id}";

            // Have your using statements within a try/catch blokc that will catch any exceptions.
            try
            {
                // We will now define your HttpClient with your first using statement which will use a IDisposable.
                using (HttpClient client = new HttpClient())
                {
                    // In the next using statement you will initiate the Get Request, use the await keyword so it will execute the using statement in order.
                    // The HttpResponseMessage which contains status code, and data from response.
                    using (HttpResponseMessage res = await client.GetAsync(baseUrl))
                    {
                        // Then get the data or content from the response in the next using statement, then within it you will get the data, and convert it to a c# object.
                        using (HttpContent content = res.Content)
                        {
                            // Return the object
                            var data = await content.ReadAsStringAsync();
                            return JObject.Parse(data);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                throw new Exception(exception.Message);
            }
        }
    }
}
