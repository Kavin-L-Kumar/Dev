using AutoMapper;
using Dev.API.Models.Domain;
using Dev.API.Models.DTO;

namespace Dev.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();
        }
    }
}
