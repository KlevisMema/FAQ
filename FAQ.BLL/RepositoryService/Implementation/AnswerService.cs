#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using FAQ.BLL.RepositoryService.Interfaces;
using FAQ.EMAIL.EmailService.ServiceInterface;
using FAQ.BLL.RepositoryService.BaseServices;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    /// <summary>
    ///     A Service class that proviced functionalities for
    ///     answer by implementing the <see cref="IAnswerService"/> interface.
    /// </summary>
    public class AnswerService : CommonServices, IAnswerService
    {
        #region Properties / Constructor / Injections
        /// <summary>
        ///     The <see cref="IEmailSender"/>
        /// </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>
        ///     Create a new instance of <see cref="AnswerService"/>.
        /// </summary>
        /// <param name="db"> The <see cref="ApplicationDbContext"/> </param>
        /// <param name="mapper"> The <see cref="IMapper"/> </param>
        public AnswerService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db,
            IEmailSender emailSender
        ) : base(mapper, log, db)
        {
            _emailSender = emailSender;
        }
        #endregion

        #region Methods implementation

        /// <summary>
        ///     Get answers of a quetion for a user, method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/>
        ///     and T is <see cref="DtoGetAnswer"/>.
        /// </returns>
        public async Task<CommonResponse<List<DtoGetAnswer>>>
        GetAnswersOfQuestion
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
        /// <summary>
        ///     Create a answer to to a question for a user, method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="dtoCreateAnswer"> the <see cref="DtoCreateAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateAnswer"/>.
        /// </returns>
        public async Task<CommonResponse<DtoCreateAnswer>>
        CreateAnswer
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

                await _log.CreateLogAction($"User with id {userId} created an answer with id  {answer.Id} for question {answer.QuestionId}", "CreateAnswer", userId);

                return CommonResponse<DtoCreateAnswer>.Response("Answers created succsessfully", true, System.Net.HttpStatusCode.OK, dtoCreateAnswer);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAnswersOfQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoCreateAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Create a answer for a answer, method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerOfAnswer"> The <see cref="DtoAnswerOfAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoAnswerOfAnswer"/>
        /// </returns>
        public async Task<CommonResponse<DtoAnswerOfAnswer>>
        CreateAnswerOfAnAnswer
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

                await _log.CreateLogAction($"User with id {userId} created an answer with id {answer.Id} for answer {answer.ParentAnswerId} of the question with id {answer.QuestionId}", "CreateAnswerOfAnAnswer", userId);

                return CommonResponse<DtoAnswerOfAnswer>.Response("Answers created succsessfully", true, System.Net.HttpStatusCode.OK, answerOfAnswer);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateAnswerOfAnAnswer", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoAnswerOfAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Edit an answer of a user
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="editAnswer"> The <see cref="DtoEditAnswer"/> </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoEditAnswer"/>.
        /// </returns>
        public async Task<CommonResponse<DtoEditAnswer>>
        EditAnswer
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

                await _log.CreateLogAction($"User with id {userId} updated an answer with id {findAnswer.Id} for question {findAnswer.QuestionId}", "EditAnswer", userId);

                return CommonResponse<DtoEditAnswer>.Response("Answers edited succsessfully", true, System.Net.HttpStatusCode.OK, editAnswer);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "EditAnswer", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoEditAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Delete an answer of a quetion of a user, method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerId"> The id of the answer </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeleteAnswer"/>.
        /// </returns>
        public async Task<CommonResponse<DtoDeleteAnswer>>
        DeleteAnswer
        (
            Guid userId,
            Guid answerId
        )
        {
            try
            {
                var answer = await _db.Answers.FirstOrDefaultAsync(x => x.Id.Equals(answerId) && x.UserId.Equals(userId.ToString()));

                if (answer is null)
                    return CommonResponse<DtoDeleteAnswer>.Response("Answer doesn't exists!", false, System.Net.HttpStatusCode.NotFound, null);

                answer.IsDeleted = true;
                answer.EditedAt = DateTime.Now;
                answer.DeletedAt = DateTime.Now;

                _db.Answers.Update(answer);
                await _db.SaveChangesAsync();

                await _log.CreateLogAction($"User with id {userId} deleted an answer with id {answer.Id} for question {answer.QuestionId}", "DeleteAnswer", userId);

                return CommonResponse<DtoDeleteAnswer>.Response("Answer deleted succsessfully!", true, System.Net.HttpStatusCode.OK, null);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteAnswer", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoDeleteAnswer>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        #endregion

    }
}