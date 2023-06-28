#region Usings
using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.BLL.RepositoryService.Interfaces
{
    /// <summary>
    ///     An interface that proviced un implemented methods in the 
    ///     context and logic of a answer.
    /// </summary>
    public interface IAnswerService
    {
        #region Methods declaration
        /// <summary>
        ///     Create a answer to to a question for a user, method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="dtoCreateAnswer"> the <see cref="DtoCreateAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateAnswer"/>.
        /// </returns>
        Task<CommonResponse<DtoCreateAnswer>> 
        CreateAnswer
        (
           Guid userId,
           DtoCreateAnswer dtoCreateAnswer
        );
        /// <summary>
        ///     Get answers of a quetion for a user, method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/>
        ///     and T is <see cref="DtoGetAnswer"/>.
        /// </returns>
        Task<CommonResponse<List<DtoGetAnswer>>> 
        GetAnswersOfQuestion
        (
            Guid userId,
            Guid questionId
        );
        /// <summary>
        ///     Create a answer for a answer, method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerOfAnswer"> The <see cref="DtoAnswerOfAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoAnswerOfAnswer"/>
        /// </returns>
        Task<CommonResponse<DtoAnswerOfAnswer>> 
        CreateAnswerOfAnAnswer
        (
            Guid userId,
            DtoAnswerOfAnswer answerOfAnswer
        );
        /// <summary>
        ///     Edit an answer of a user
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="editAnswer"> The <see cref="DtoEditAnswer"/> </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoEditAnswer"/>.
        /// </returns>
        Task<CommonResponse<DtoEditAnswer>> 
        EditAnswer
        (
           Guid userId,
           DtoEditAnswer editAnswer
        );
        /// <summary>
        ///     Delete an answer of a quetion of a user, method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerId"> The id of the answer </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeleteAnswer"/>.
        /// </returns>
        Task<CommonResponse<DtoDeleteAnswer>> 
        DeleteAnswer
        (
            Guid userId,
            Guid answerId
        ); 
        #endregion
    }
}