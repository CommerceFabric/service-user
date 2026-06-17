using AutoMapper;
using eCommerce.Core.DTOs;
using eCommerce.Core.Entities;

namespace eCommerce.Core.Mappers
{
    public class ApplicationUserMappingProfile : Profile
    {
        public ApplicationUserMappingProfile()
        {
            CreateMap<ApplicationUser, AuthenticationResponse>()
                // required to map the properties of ApplicationUser to the constructor parameters of AuthenticationResponse (due to it being a RECORD, and these parameters not existing in the source)
                .ForCtorParam(nameof(AuthenticationResponse.Token),
                    opt => opt.MapFrom(_ => (string?)null))
                .ForCtorParam(nameof(AuthenticationResponse.Success),
                    opt => opt.MapFrom(_ => false));

            CreateMap<RegisterRequest, ApplicationUser>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender.ToString()));
        }
    }
}
