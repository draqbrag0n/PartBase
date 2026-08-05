using System.Net.Http.Json;
using PartBase.Application.Common;
using PartBase.Application.DTOs.Components;

namespace PartBase.Web.Services;

public class ComponentApiService
{
    private readonly IHttpClientFactory _factory;

    public ComponentApiService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<PagedResult<ComponentDto>?> GetComponentsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 20)
    {
        var client = _factory.CreateClient("PartBaseApi");

        var url =
            $"api/components?search={Uri.EscapeDataString(search ?? "")}&page={page}&pageSize={pageSize}";

        return await client.GetFromJsonAsync<PagedResult<ComponentDto>>(url);
    }

    public async Task<ComponentDto?> GetByIdAsync(Guid id)
    {
        var client = _factory.CreateClient("PartBaseApi");

        return await client.GetFromJsonAsync<ComponentDto>(
            $"api/components/{id}");
    }

    public async Task<ComponentDto?> CreateAsync(CreateComponentRequest request)
    {
        var client = _factory.CreateClient("PartBaseApi");

        var response = await client.PostAsJsonAsync("api/components", request);

        if (!response.IsSuccessStatusCode)
            return null;

        return await response.Content.ReadFromJsonAsync<ComponentDto>();
    }

    public async Task<bool> UpdateAsync(Guid id, CreateComponentRequest request)
    {
        var client = _factory.CreateClient("PartBaseApi");

        var response = await client.PutAsJsonAsync(
            $"api/components/{id}",
            request);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var client = _factory.CreateClient("PartBaseApi");

        var response = await client.DeleteAsync($"api/components/{id}");

        return response.IsSuccessStatusCode;
    }
}