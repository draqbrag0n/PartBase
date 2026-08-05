using PartBase.Application.Common;

namespace PartBase.Application.Interfaces;

public interface ICategoryService
{
    Task<List<LookupDto>> GetAllAsync();
}