using Dev.API.Models.Domain;
using Dev.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dev.API.Repositories
{
    public interface IRegionRepository
    {
        Task<List<Region>> getAll();

        Task<Region?> getById(Guid id);

        Task<List<Region>> create(InputRegionDTO[] inputRegions);

        Task<Region?> update(Guid id, Region inputRegions);

        Task<Region?> delete(Guid id);
    }
}
