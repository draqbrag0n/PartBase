using PartBase.Application.DTOs;
using PartBase.Application.DTOs.Components;

namespace PartBase.Application.Interfaces;

public interface IComponentService
{
    Task<PagedResult<ComponentDto>> GetAllAsync(string? search, int page, int pageSize);

    Task<ComponentDto?> GetByIdAsync(Guid id);

    Task<ComponentDto> CreateAsync(CreateComponentRequest request);

    Task<bool> UpdateAsync(Guid id, CreateComponentRequest request);

    Task<bool> DeleteAsync(Guid id);
}