using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.QuestionsDtos;

namespace FAQ.DTO.Mappings
{
    public class QuestionTagMappings : Profile
    {
        public QuestionTagMappings() 
        {
            CreateMap<QuestionTag, DtoCreateQuestion>()
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(q => q.TagId));
        }

    }
}