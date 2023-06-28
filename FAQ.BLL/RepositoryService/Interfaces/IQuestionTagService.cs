#region Usings
using FAQ.DTO.QuestionsDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.BLL.RepositoryService.Interfaces
{
    /// <summary>
    ///     An interface that proviced un implemented methods in the 
    ///     context and logic of a Qestion tag.
    /// </summary>
    public interface IQuestionTagService
    {
        #region Methods declaration
        /// <summary>
        ///     Create a question tag method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="dtoCreateQuestion"> 
        ///     The <see cref="DtoCreateQuestionReturn"/> object 
        /// </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoCreateQuestion>>
        CreateQuestionTag
        (
           Guid userId,
           DtoCreateQuestionReturn dtoCreateQuestion
        );
        #endregion
    }
}