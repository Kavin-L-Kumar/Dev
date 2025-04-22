using Dev.API.Models.Domain;
using Dev.API.Models.DTO;
using Microsoft.EntityFrameworkCore;

namespace Dev.API.Repositories
{
    public class DuplicateRegionRepositary : IRegionRepository
    {
        private readonly DevDbContext _repository;

        public DuplicateRegionRepositary(DevDbContext repo)
        {
            this._repository = repo;
        }
        public async Task<List<Region>> create(InputRegionDTO[] inputRegions)
        {
            var regionToBeAdded = new List<Region>();

            foreach (var regions in inputRegions)
            {
                var region = new Region
                {
                    Code = regions.Code,
                    RegionUrl = regions.RegionUrl,
                    Name = regions.Name
                };
                regionToBeAdded.Add(region);
                await _repository.AddAsync(region);

            }
            await _repository.SaveChangesAsync();
            return regionToBeAdded;
        }

        public async Task<Region?> delete(Guid id)
        {
            var resourceToRemove = await _repository.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (resourceToRemove == null) return null;

            _repository.Regions.Remove(resourceToRemove);
            await _repository.SaveChangesAsync();
            return resourceToRemove;
        }

        public async Task<List<Region>> getAll()
        {
            return await _repository.Regions.ToListAsync();
        }

        public async Task<Region?> getById(Guid id)
        {
            return await _repository.Regions.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Region?> update(Guid id, Region inputRegions)
        {
            var resourceToUpdate = await _repository.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (resourceToUpdate == null) return null;

            resourceToUpdate.RegionUrl = inputRegions.RegionUrl;
            resourceToUpdate.Name = inputRegions.Name;
            resourceToUpdate.Code = inputRegions.Code;

            await _repository.SaveChangesAsync();
            return resourceToUpdate;
        }
    }
}
