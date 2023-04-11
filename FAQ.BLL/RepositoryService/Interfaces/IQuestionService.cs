using FAQ.DTO.QuestionsDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.BLL.RepositoryService.Interfaces
{
    public interface IQuestionService
    {
        Task<CommonResponse<DtoCreateQuestion>> CreateQuestion
        (
            Guid userId,
            DtoCreateQuestion newQuestion
        );
        Task<CommonResponse<DtoDeletedQuestion>> DeleteQuestion
        (
            Guid userId,
            Guid questionId
        );
        Task<CommonResponse<List<DtoGetQuestion>>> GetAllQuestions
        (
            Guid userId
        );
        Task<CommonResponse<DtoGetQuestion>> GetQuestion
        (
            Guid userId,
            Guid qestionId
        );
        Task<CommonResponse<DtoUpdateQuestion>> UpdateQuestion
        (
            Guid userId,
            DtoUpdateQuestion question
        );
        Task<CommonResponse<DtoDisabledQuestion>> DisableQuestion
        (
            Guid userId,
            Guid questionId
        );

        Task<CommonResponse<List<DtoDisabledQuestion>>> GetAllDisabledQuesions
        (
            Guid userId
        );
        Task<CommonResponse<DtoDisabledQuestion>> GetDisabledQuesions
       (
           Guid userId,
           Guid questionId
       );
    }
}