using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using System.Net;
using Newtonsoft.Json;

namespace Tests.API
{
    public class DishAPI
    {

        [Fact]
        public void Get_Bad_Request()
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var x = apiClient.GetAsync("https://easy-meal-sswp-api.azurewebsites.net/api/dish/badrequest").Result.StatusCode;

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
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Theory]
        [InlineData(10)]
        public async Task Delete_Dish_Not_Implemented(int? id = null)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.DeleteAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/{id}");

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Post_Dish_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PostAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task Patch_Dish_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PatchAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.MethodNotAllowed, response.StatusCode);
        }

        [Fact]
        public async Task Put_Dish_Not_Implemented()
        {
            // Arrange
            var apiClient = new HttpClient();
            HttpContent content = null;

            // Act
            var response = await apiClient.PutAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/", content);

            // Assert
            Assert.True(!response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_Dish_By_Id_1_Is_OK(int? id = 1)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net/api/Dish/{id}");

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData(1)]
        public async Task Get_Dish_By_Id_Status_200(int? id = 1)
        {
            // Arrange
            var apiClient = new HttpClient();

            // Act
            var response = await apiClient.GetAsync($"https://easy-meal-sswp-api.azurewebsites.net//api/Dish/{id}");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(HttpStatusCode.InternalServerError, response.StatusCode);
        }

        [Fact]
        public async Task Get_All_Dishes()
        {
            var apiClient = new HttpClient();

            var apiResponse = await apiClient.GetAsync("https://easy-meal-sswp-api.azurewebsites.net/api/Dish");

            Assert.True(apiResponse.IsSuccessStatusCode);

        }
    }
}