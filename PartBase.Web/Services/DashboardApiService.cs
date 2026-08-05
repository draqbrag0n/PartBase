using System.Net.Http.Json;
using PartBase.Application.DTOs.Dashboard;

namespace PartBase.Web.Services;

public class DashboardApiService
{
    private readonly IHttpClientFactory _factory;

    public DashboardApiService(IHttpClientFactory factory)
    {
        _factory = factory;
    }

    public async Task<DashboardDto?> GetAsync()
    {
        var client = _factory.CreateClient("PartBaseApi");

        return await client.GetFromJsonAsync<DashboardDto>("api/dashboard");
    }
}