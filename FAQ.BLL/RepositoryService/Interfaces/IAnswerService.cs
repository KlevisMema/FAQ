using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.BLL.RepositoryService.Interfaces
{
    public interface IAnswerService
    {
        Task<CommonResponse<DtoCreateAnswer>> CreateAnswer(Guid userId, DtoCreateAnswer dtoCreateAnswer);
        Task<CommonResponse<List<DtoGetAnswer>>> GetAnswersOfQuestion(Guid userId, Guid questionId);
    }
}