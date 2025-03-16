using OrderService.Model;

namespace OrderService.Data
{
    public class UserClient
    {
        private readonly HttpClient _httpClient;

        public UserClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDTO> GetUserById(int userId)
        {
            var response = await _httpClient.GetAsync($"http://localhost:5246/api/Users/{userId}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<UserDTO>();
        }

    }
}
