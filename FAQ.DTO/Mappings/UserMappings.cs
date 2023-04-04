#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.UserDtos;
using Microsoft.AspNetCore.Identity;
#endregion

namespace FAQ.DTO.Mappings
{
    /// <summary>
    ///     A user mapper class that provides transforming <see cref="User"/> types 
    ///     to other types related to the user and the opposite.
    /// </summary>
    public class UserMappings : Profile
    {
        public UserMappings()
        {
            #region Mappings

            // It will translate the DtoRegister type to User type.
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

            // It will translate the DtoLogin type to User type.
            CreateMap<DtoLogin, User>();

            // It will translate the IdentityRole type to DtoRoles type.
            CreateMap<IdentityRole, DtoRoles>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            #endregion
        }
    }
}