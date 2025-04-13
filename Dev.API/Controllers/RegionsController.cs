using Dev.API.Models.Domain;
using Dev.API.Repositary;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult getAll()
        {
            var regions = Repositary.Regions.Select(x => x);
            return Ok(regions);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult getRegionWithId([FromRoute] Guid id)
        {
            var regions = Repositary.Regions.Where(x=>x.Id == id);

            if(!regions.Any()) return NotFound();
            return Ok(regions);

        }
    }
}
