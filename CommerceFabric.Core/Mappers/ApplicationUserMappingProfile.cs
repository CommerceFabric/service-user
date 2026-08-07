using AutoMapper;
using CommerceFabric.Core.DTOs;
using CommerceFabric.Core.Entities;

namespace CommerceFabric.Core.Mappers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, UpdateUserDetailsRequest>();
        }
    }
}
