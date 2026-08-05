using PartBase.Application.Common;

namespace PartBase.Application.Interfaces;

public interface IManufacturerService
{
    Task<List<LookupDto>> GetAllAsync();
}