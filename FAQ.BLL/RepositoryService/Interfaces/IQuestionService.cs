#region Usings
using FAQ.DTO.QuestionsDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.BLL.RepositoryService.Interfaces
{
    /// <summary>
    ///     An interface that provides un implemented 
    ///     methods in the context of the logic of question.
    /// </summary>
    public interface IQuestionService
    {
        #region Methods declaration
        /// <summary>
        ///     Create a question for a user method declaration.
        /// </summary>
        /// <param name="userId"> The user Id </param>
        /// <param name="newQuestion"> The <see cref="DtoCreateQuestion"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateQuestionReturn"/>
        /// </returns>
        Task<CommonResponse<DtoCreateQuestionReturn>>
        CreateQuestion
        (
           Guid userId,
           DtoCreateQuestion newQuestion
        );
        /// <summary>
        ///     Get all questions from questions table
        ///     method declaration.that are not disabled.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id. </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="List{T}"/> and T is <see cref="DtoGetQuestion"/>. 
        /// </returns>
        Task<CommonResponse<List<DtoGetQuestion>>>
        GetAllNonDisabledQuestions
        (
          Guid userId
        );
        /// <summary>
        ///     Delete a question of a user method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The question id </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeletedQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoDeletedQuestion>>
        DeleteQuestion
        (
            Guid userId,
            Guid questionId
        );
        /// <summary>
        ///     Get all questions from questions table
        ///     method declaration.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id. </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="List{T}"/> and T is <see cref="DtoGetQuestion"/>. 
        /// </returns>
        Task<CommonResponse<List<DtoGetQuestion>>>
        GetAllQuestions
        (
            Guid userId
        );
        /// <summary>
        ///     Get a question from questions table
        ///     method declaration.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="DtoGetQuestion"/>.
        /// </returns>
        Task<CommonResponse<DtoGetQuestion>>
        GetQuestion
        (
            Guid userId,
            Guid qestionId
        );
        /// <summary>
        ///     Update a question of a user method declaration.
        /// </summary>
        /// <param name="userId"> The user Id </param>
        /// <param name="question"> The <see cref="DtoUpdateQuestion"/> </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoUpdateQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoUpdateQuestion>>
        UpdateQuestion
        (
            Guid userId,
            DtoUpdateQuestion question
        );
        /// <summary>
        ///     Disable a question of a user method declaration.        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <param name="questionId"> The question id</param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoDisabledQuestion>>
        DisableQuestion
        (
            Guid userId,
            Guid questionId
        );
        /// <summary>
        ///     Get all the disabled question of a user method declaration.
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/> and 
        ///     T is <see cref="DtoDisabledQuestion"/>
        /// </returns>
        Task<CommonResponse<List<DtoDisabledQuestion>>>
        GetAllDisabledQuesions
        (
            Guid userId
        );
        /// <summary>
        ///     Get all disabled quesitons of a user method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoDisabledQuestion>>
        GetDisabledQuesion
        (
           Guid userId,
           Guid questionId
        );
        /// <summary>
        ///     Un disable a question of a user method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        Task<CommonResponse<DtoDisabledQuestion>>
        UnDisableQuestion
        (
            Guid userId,
            Guid questionId
        );
        /// <summary>
        ///     Get question with answers and answers with answers method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>
        /// </returns>
        Task<CommonResponse<DtoQuestionAnswers>>
        GetQuestionWithAnswersAndChildAnswers
        (
            Guid userId,
            Guid questionId
        );
        /// <summary>
        ///     Get questions with answers method declaration.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>
        /// </returns>
        Task<CommonResponse<DtoQuestionAnswers>>
        GetQuestionWithAnswersNoChildAnswers
        (
             Guid userId,
            Guid questionId
        );
        #endregion
    }
}