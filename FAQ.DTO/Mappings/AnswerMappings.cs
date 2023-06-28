#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.AnswerDtos;
#endregion

namespace FAQ.DTO.Mappings
{
    /// <summary>
    ///    A class that provides all the necessary mappings for all the dtos 
    ///    of the <see cref="Answer"/> model and in reverse.
    /// </summary>
    public class AnswerMappings : Profile
    {
        /// <summary>
        ///     The <see cref="AnswerMappings"/> constructor.
        ///     Instasiate a new insance of <see cref="AnswerMappings"/> and configure mappings.
        /// </summary>
        public AnswerMappings()
        {
            #region Mappings
            // It will translate the Answer type to DtoGetAnswer type.
            CreateMap<Answer, DtoGetAnswer>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Answer, opt => opt.MapFrom(src => src.P_Answer));

            // It will translate the DtoCreateAnswer type to Answer type.
            CreateMap<DtoCreateAnswer, Answer>()
               .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer));

            // It will translate the DtoAnswerOfAnswer type to Answer type.
            CreateMap<DtoAnswerOfAnswer, Answer>()
               .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
               .ForMember(dest => dest.P_Answer, opt => opt.MapFrom(src => src.Answer))
               .ForMember(dest => dest.ParentAnswerId, opt => opt.MapFrom(src => src.ParentAnswerId));
            #endregion
        }
    }
}