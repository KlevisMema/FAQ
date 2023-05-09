#region Usings
using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
#endregion

namespace FAQ.BLL.RepositoryService.Interfaces
{
    public interface IAnswerService
    {
        Task<CommonResponse<DtoCreateAnswer>> CreateAnswer
        (
            Guid userId,
            DtoCreateAnswer dtoCreateAnswer
        );
        Task<CommonResponse<List<DtoGetAnswer>>> GetAnswersOfQuestion
        (
            Guid userId,
            Guid questionId
        );
        Task<CommonResponse<DtoAnswerOfAnswer>> CreateAnswerOfAnAnswer
        (
            Guid userId,
            DtoAnswerOfAnswer answerOfAnswer
        );
        Task<CommonResponse<DtoEditAnswer>> EditAnswer
        (
           Guid userId,
           DtoEditAnswer editAnswer
        );
        Task<CommonResponse<DtoDeleteAnswer>> DeleteAnswer
        (
            Guid userId,
            Guid answerId
        );
    }
}