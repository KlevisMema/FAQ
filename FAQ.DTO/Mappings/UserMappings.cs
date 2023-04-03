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
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IsAdmin, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.TwoFactorEnabled, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => false))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Prefix, opt => opt.MapFrom(src => src.PhonePrefix))
                .ForMember(dest => dest.Adress, opt => opt.MapFrom(src => src.Adress));

            CreateMap<DtoLogin, User>();

            CreateMap<IdentityRole, DtoRoles>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        }
    }
}
