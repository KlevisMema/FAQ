using FAQ.DTO.QuestionsDtos;
using FAQ.SHARED.ResponseTypes;

namespace FAQ.BLL.RepositoryService.Interfaces
{
    public interface IQuestionTagService
    {
        Task<CommonResponse<DtoCreateQuestion>> CreateQuestionTag(Guid userId, DtoCreateQuestionReturn dtoCreateQuestion);
    }
}