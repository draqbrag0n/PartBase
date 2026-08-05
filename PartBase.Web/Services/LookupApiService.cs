using PartBase.Application.Common;
using System.Net.Http.Json;

namespace PartBase.Web.Services;

public class LookupApiService
{
    private readonly IHttpClientFactory _factory;

    public LookupApiService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<List<LookupDto>> GetManufacturersAsync()
    {
        var client = _factory.CreateClient("PartBaseApi");

        return await client.GetFromJsonAsync<List<LookupDto>>("api/manufacturers")
               ?? new List<LookupDto>();
    }

    public async Task<List<LookupDto>> GetCategoriesAsync()
    {
        var client = _factory.CreateClient("PartBaseApi");

        return await client.GetFromJsonAsync<List<LookupDto>>("api/categories")
               ?? new List<LookupDto>();
    }
}