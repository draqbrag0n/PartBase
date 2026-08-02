using PartBase.Domain.Entities;

namespace PartBase.Application.Interfaces;

public interface ICategoryService
{
    Task<List<Category>> GetAllAsync();
}