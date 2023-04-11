#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.QuestionsDtos;
#endregion

namespace FAQ.DTO.Mappings
{
    public class QuestionMappings : Profile
    {
        public QuestionMappings()
        {
            CreateMap<Question, DtoGetQuestion>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.EditedAt))
                .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.CreatedAt))
                .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));


            CreateMap<DtoCreateQuestion, Question>()
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));


            CreateMap<DtoUpdateQuestion, Question>()
               .ForMember(dest => dest.EditedAt, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));

            CreateMap<Question, DtoDeletedQuestion>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));

            CreateMap<Question, DtoDisabledQuestion>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));
        }
    }
}