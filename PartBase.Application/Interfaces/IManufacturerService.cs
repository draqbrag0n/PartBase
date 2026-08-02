using PartBase.Domain.Entities;

namespace PartBase.Application.Interfaces;

public interface IManufacturerService
{
    Task<List<Manufacturer>> GetAllAsync();
}