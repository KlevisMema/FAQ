#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.QuestionsDtos;
#endregion

namespace FAQ.DTO.Mappings
{
    /// <summary>
    ///    A class that provides all the necessary mappings for all the dtos 
    ///    of the <see cref="QuestionTag"/> model and in reverse.
    /// </summary>
    public class QuestionTagMappings : Profile
    {
        /// <summary>
        ///     The <see cref="QuestionTagMappings"/> constructor.
        ///     Instasiate a new insance of <see cref="QuestionTagMappings"/> and configure mappings.
        /// </summary>
        public QuestionTagMappings()
        {
            #region Mappings
            // It will translate the QuestionTag type to DtoCreateQuestion type.
            CreateMap<QuestionTag, DtoCreateQuestion>()
                .ForMember(dest => dest.TagId, opt => opt.MapFrom(q => q.TagId));
            #endregion
        }

    }
}