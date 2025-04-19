using AutoMapper;
using Dev.API.Models.Domain;
using Dev.API.Models.DTO;
using Dev.API.Repositories;
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
        private readonly IRegionRepository repositaryPattern;
        private readonly IMapper mapper;

        public RegionsController(DevDbContext repositary, IRegionRepository repositaryPatter, IMapper mapper)
        {
            this.Repositary = repositary;
            this.repositaryPattern = repositaryPatter;
            this.mapper = mapper;
        }

        public DevDbContext Repositary { get; }

        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            List<Region> regions = await repositaryPattern.getAll();

            var regionDTO = mapper.Map<List<RegionDTO>>(regions);

            if (!regionDTO.Any()) return NotFound();
            return Ok(regionDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> getRegionWithId([FromRoute] Guid id)
        {
            var regions = await repositaryPattern.getById(id);

            if (regions?.Id == null) return NotFound();
            var regionDTO = mapper.Map<RegionDTO>(regions);

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
            var regionDTOForReturn = new List<RegionDTO>();

            var createdRegions = await repositaryPattern.create(inputRegion);

            foreach (var RegionDTO in createdRegions)
            {
                var RegionToReturn = mapper.Map<RegionDTO>(RegionDTO);
                regionDTOForReturn.Add(RegionToReturn);
            }

            return StatusCode(201, regionDTOForReturn);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateRegions([FromRoute] Guid id, [FromBody] InputRegionDTO inputRegionDTO)
        {
            var regionForUpdate = mapper.Map<Region>(inputRegionDTO);

            var updatedRegion = await repositaryPattern.update(id, regionForUpdate);

            // Changing Domain models to DTO for return
            if(updatedRegion == null) return NotFound();

            var regionDto = mapper.Map<RegionDTO>(updatedRegion);
            return Ok(regionDto);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteRegion([FromRoute] Guid id)
        {
            var regionForDelete = repositaryPattern.delete(id);
            if (regionForDelete == null) return NotFound();

            return Ok();
        }
    }
}
