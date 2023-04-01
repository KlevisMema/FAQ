using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using Microsoft.AspNetCore.Identity;

namespace FAQ.DTO.Mappings
{
    public class UserMappings : Profile
    {
        public UserMappings()
        {

            CreateMap<DtoRegister, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<DtoLogin, User>();

            CreateMap<IdentityRole, DtoRoles>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
