using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.AnswerDtos;

namespace FAQ.DTO.Mappings
{
    public class AnswerMappings : Profile
    {
        public AnswerMappings()
        {
            CreateMap<Answer, DtoGetAnswer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.P_Answer));

            CreateMap<DtoCreateAnswer, Answer>()
               .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer));

        }
    }
}