using Dev.API.Models.Domain;
using Dev.API.Models.DTO;
using Dev.API.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Dev.API.Controllers
{
    // https:localhost:911/api/Resions
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        public RegionsController(DevDbContext repositary)
        {
            Repositary = repositary;
        }

        public DevDbContext Repositary { get; }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            var regions = await Repositary.Regions.Select(x => x).ToListAsync();

            var regionDTO = new List<RegionDTO>();
            foreach (var regionData in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regionData.Id,
                    Code = regionData.Code,
                    RegionUrl = regionData.RegionUrl
                });
            }

            if (!regionDTO.Any()) return NotFound();
            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getRegionWithId([FromRoute] Guid id)
        {
            var regions = await Repositary.Regions.Where(x => x.Id == id).ToListAsync();

            var regionDTO = new List<RegionDTO>();
            foreach (var regionData in regions)
            {
                regionDTO.Add(new RegionDTO()
                {
                    Id = regionData.Id,
                    Code = regionData.Code,
                    RegionUrl = regionData.RegionUrl
                });
            }

            if (!regionDTO.Any()) return NotFound();
            return Ok(regionDTO);

        }

        [HttpPost]
        public async Task<IActionResult> createRegion([FromBody] InputRegionDTO[] inputRegion)
        {
            if (inputRegion == null)
            {
                return BadRequest("Request body is missing.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var regionToBePersist = new List<Region>();
            var regionDTOForReturn = new List<RegionDTO>();

            foreach (var regions in inputRegion)
            {
                var region = new Region
                {
                    Code = regions.Code,
                    RegionUrl = regions.RegionUrl,
                    Name = regions.Name
                };
                regionToBePersist.Add(region);
                Repositary.AddAsync(region);

            }
            await Repositary.SaveChangesAsync();

            /* Using the DTO to return instead of the Domain(regionToBePersist)*/
            
            foreach(var RegionDTO in regionToBePersist)
            {
                var RegionToReturn = new RegionDTO
                {
                    Id = RegionDTO.Id,
                    Code = RegionDTO.Code,
                    RegionUrl = RegionDTO.RegionUrl,
                    Name = RegionDTO.Name
                };
                regionDTOForReturn.Add(RegionToReturn);
            }

            return StatusCode(201, regionDTOForReturn);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegions([FromRoute] Guid id, [FromBody] InputRegionDTO inputRegionDTO)
        {
            var resourceToUpdate = await Repositary.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(resourceToUpdate == null) return NotFound();

            resourceToUpdate.RegionUrl = inputRegionDTO.RegionUrl;
            resourceToUpdate.Name = inputRegionDTO.Name;
            resourceToUpdate.Code = inputRegionDTO.Code;

            Repositary.SaveChanges();

            // Changing Domain models to DTO for return

            var regionDto = new RegionDTO
            {
                Id = resourceToUpdate.Id,
                Code = resourceToUpdate.Code,
                RegionUrl = resourceToUpdate.RegionUrl,
                Name = resourceToUpdate.Name
            };

            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var resourceToRemove = await Repositary.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if(resourceToRemove == null) return NotFound();

            Repositary.Regions.Remove(resourceToRemove);
            Repositary.SaveChanges();

            return Ok();
        }
    }
}
