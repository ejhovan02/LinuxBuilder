using System.Net.Http.Json;
using LinuxBuilder.Shared.Models;

public class TemplateService
{
    private readonly HttpClient _httpClient;

    public TemplateService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Package>> GetPackagesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<Package>>("api/template/packages");
    }

    public async Task<List<OsTemplate>> GetOsTemplatesAsync()
    {
        return await _httpClient.GetFromJsonAsync<List<OsTemplate>>("api/template/os-templates");
    }

    public async Task<OsTemplate> CreateOsTemplateAsync(OsTemplate osTemplate)
    {
        var response = await _httpClient.PostAsJsonAsync("api/template", osTemplate);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<OsTemplate>();
    }
}