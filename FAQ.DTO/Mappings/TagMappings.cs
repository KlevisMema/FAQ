#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DTO.TagDtos;
#endregion

namespace FAQ.DTO.Mappings
{
    /// <summary>
    ///     A tag mapper class that provides transforming <see cref="Tag"/> types 
    ///     to other types related to the tags and the opposite.
    /// </summary>
    public class TagMappings : Profile
    {
        /// <summary>
        ///     Instasiate a new insance of <see cref="TagMappings"/>
        ///     and configure mappings.
        /// </summary>
        public TagMappings()
        {
            #region Mappings
            // Tansform Tag obj to DtoTag obj.
            CreateMap<Tag, DtoTag>();
            #endregion
        }
    }
}