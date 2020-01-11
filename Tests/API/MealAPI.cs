using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Newtonsoft.Json;

namespace Tests.API
{
    public class MealAPI
    {

        [Fact]
        public void Get_Bad_Request()
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var x = apiClient.GetAsync("https://easy-meal-sswp-api.azurewebsites.net/api/Meal/badrequest").Result.StatusCode;

            // Deserialize and examine results.
            Assert.Equal(HttpStatusCode.BadRequest, x);
        }

        [Theory]
        [InlineData(500)]
        public async Task Get_Exception_Not_Found(int? id = null)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory]
        [InlineData(10)]
        public async Task Delete_Meal_Not_Implemented(int? id = null)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.DeleteAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/{id}");

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Post_Meal_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PostAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Fact]
        public async Task Patch_Meal_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PatchAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task Put_Meal_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PutAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.UnsupportedMediaType, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_Meal_By_Id_1_Is_OK(int? id = 1)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Meal/{id}");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_Meal_By_Id_Status_200(int? id = 1)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net//api/Meal/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Meals()
        {
            var apiClient = new HttpClient();

            var apiResponse = await apiClient.GetAsync("https://easy-meal-sswp-api.azurewebsites.net/api/Meal");

            Assert.True(apiResponse.IsSuccessStatusCode);

        }
    }
}