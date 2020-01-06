using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Domain;
using System.Collections.Generic;
using System.Linq;

namespace Client.Extentsions.Meal
{
    public static class MealMethods
    {

        public async static Task<IEnumerable<Domain.Meal>> GetAllWeekMeals(this DateTime date)
        {
            // Fetching Dishes into local JArray
            JArray mealArray = await GetMeals();
            // Converting JArray items to Collection object of given type
            List<Domain.Meal> allMeals = mealArray.ToObject<List<Domain.Meal>>();

            return allMeals.Where(m => Week(m.DateValid) == Week(date));
        }

        public static int Week(this DateTime date)
        {
            GregorianCalendar cal = new GregorianCalendar(GregorianCalendarTypes.Localized);
            return cal.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
        }

        // Now define your asynchronous method which will retrieve all your pokemon.
        public static async Task<JArray> GetMeals()
        {
            // Define your baseUrl
            string baseUrl = "https://easy-meal-sswp-api.azurewebsites.net/api/Meal";

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

        public static async Task<JObject> GetMealById(this int id)
        {
            // Define your baseUrl with index
            string baseUrl = $"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/{id}";

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
