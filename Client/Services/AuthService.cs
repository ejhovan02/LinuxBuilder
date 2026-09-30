using System.Net.Http.Json;
using LinuxBuilder.Shared.Models;
using LinuxBuilder.Client.Models;

public class AuthService
{
    private readonly HttpClient _httpClient;

    public AuthService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<HttpResponseMessage> RegisterAsync(RegisterModel registerModel)
    {
        var response = await _httpClient.PostAsJsonAsync("api/auth/register", registerModel);
        return response;
    }

    public async Task<LoginResponse?> LoginAsync(LoginModel loginModel)
    {
        var response = await _httpClient.PostAsJsonAsync(
            "api/auth/login",
            loginModel);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<LoginResponse>();
    }
}