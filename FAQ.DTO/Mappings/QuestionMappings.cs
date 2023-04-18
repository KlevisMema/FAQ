#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.AnswerDtos;
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
                .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Tittle))
                .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDeleted))
                .ForMember(dest => dest.DtoQuestionTags, opt => opt.MapFrom(src => src.QuestionTags!.Select(x => new DtoQuestionTag
                {
                    TagId = x.TagId,
                    TagName = x.Tag!.Name
                })));

            CreateMap<DtoCreateQuestion, Question>()
               .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.Tittle))
               .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));

            CreateMap<Question, DtoCreateQuestionReturn>()
                .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.Id));


            CreateMap<DtoUpdateQuestion, Question>()
               .ForMember(dest => dest.Tittle, opt => opt.MapFrom(src => src.Tittle))
               .ForMember(dest => dest.EditedAt, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));

            CreateMap<Question, DtoDeletedQuestion>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question));

            CreateMap<Question, DtoDisabledQuestion>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.DeletedAt, opt => opt.MapFrom(src => src.DeletedAt))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Tittle))
               .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDeleted))
               .ForMember(dest => dest.DtoQuestionTags, opt => opt.MapFrom(src => src.QuestionTags!.Select(x => new DtoQuestionTag
               {
                   TagId = x.TagId,
                   TagName = x.Tag!.Name
               })));

            CreateMap<Question, DtoQuestionAnswers>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Created, opt => opt.MapFrom(src => src.CreatedAt))
               .ForMember(dest => dest.Edited, opt => opt.MapFrom(src => src.EditedAt))
               .ForMember(dest => dest.P_Question, opt => opt.MapFrom(src => src.P_Question))
               .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Tittle))
               .ForMember(dest => dest.Disabled, opt => opt.MapFrom(src => src.IsDeleted))
               .ForMember(dest => dest.DtoQuestionTags, opt => opt.MapFrom(src => src.QuestionTags!.Select(x => new DtoQuestionTag
               {
                   TagId = x.TagId,
                   TagName = x.Tag!.Name
               })))
               .ForMember(dest => dest.DtoAnswers, opt => opt.MapFrom(src => src.Answers!.Select(a => new DtoAnswer
               {
                   Answer = a.P_Answer,
                   Id = a.Id,
                   ChildAnswers = a.ChildAnswers!.Select(ca => new DtoAnswer
                   {
                       Id = ca.Id,
                       Answer = ca.P_Answer,
                       ChildAnswers = ca.ChildAnswers!.Select(cca => new DtoAnswer
                       {
                           Id = cca.Id,
                           Answer = cca.P_Answer,
                       }).ToList(),
                   }).ToList()
               }).ToList()
               ));
        }
    }
}