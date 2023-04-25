#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using FAQ.BLL.RepositoryService.Interfaces;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    /// <summary>
    /// 
    /// </summary>
    public class AnswerService : IAnswerService
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _db;
        private readonly ILogService _log;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <param name="mapper"></param>
        /// <param name="logService"></param>
        public AnswerService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db
        )
        {
            _db = db;
            _mapper = mapper;
            _log = log;
        }

        public async Task<CommonResponse<List<DtoGetAnswer>>> GetAnswersOfQuestion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var answers = await _db.Answers.Include(x => x.Question)
                                               .Where(x => x.QuestionId.Equals(questionId))
                                               .ToListAsync();

                if (answers is null)
                    return CommonResponse<List<DtoGetAnswer>>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoAnswer = _mapper.Map<List<DtoGetAnswer>>(answers);

                return CommonResponse<List<DtoGetAnswer>>.Response("Answers retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoAnswer);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAnswersOfQuestion", userId);

                return CommonResponse<List<DtoGetAnswer>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoCreateAnswer>> CreateAnswer
        (
            Guid userId,
            DtoCreateAnswer dtoCreateAnswer
        )
        {
            try
            {
                var answer = _mapper.Map<Answer>(dtoCreateAnswer);

                answer.UserId = userId.ToString();

                _db.Add(answer);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoCreateAnswer>.Response("Answers created succsessfully", true, System.Net.HttpStatusCode.OK, dtoCreateAnswer);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAnswersOfQuestion", userId);

                return CommonResponse<DtoCreateAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoAnswerOfAnswer>> CreateAnswerOfAnAnswer
        (
            Guid userId,
            DtoAnswerOfAnswer answerOfAnswer
        )
        {
            try
            {
                var answer = _mapper.Map<Answer>(answerOfAnswer);

                answer.UserId = userId.ToString();

                _db.Add(answer);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoAnswerOfAnswer>.Response("Answers created succsessfully", true, System.Net.HttpStatusCode.OK, answerOfAnswer);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateAnswerOfAnAnswer", userId);

                return CommonResponse<DtoAnswerOfAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoEditAnswer>> EditAnswer
        (
            Guid userId,
            DtoEditAnswer editAnswer
        )
        {
            try
            {
                var findAnswer = await _db.Answers.FirstOrDefaultAsync(x => x.Id.Equals(editAnswer.Id) && x.UserId.Equals(userId.ToString()));

                if (findAnswer is null)
                    return CommonResponse<DtoEditAnswer>.Response("Answer doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                findAnswer.P_Answer = editAnswer.Answer;
                findAnswer.EditedAt = DateTime.Now;

                _db.Answers.Update(findAnswer);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoEditAnswer>.Response("Answers edited succsessfully", true, System.Net.HttpStatusCode.OK, editAnswer);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "EditAnswer", userId);

                return CommonResponse<DtoEditAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        //public async Task<CommonResponse<>> DeleteAnswer
        //(
        //    Guid userId,
        //    Guid answerId
        //)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception ex)
        //    {
        //        await _log.CreateLogException(ex, "DeleteAnswer", userId);

        //        return CommonResponse<DtoAnswerOfAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
        //    }
        //}
    }
}