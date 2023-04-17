
#region Usings
using AutoMapper;
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.DTO.QuestionsDtos;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using FAQ.BLL.RepositoryService.Interfaces;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    public class QuestionService : IQuestionService
    {

        #region Services Injection
        /// <summary>
        ///     The <see cref="ILogService"/>.
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     The <see cref="ApplicationDbContext"/>.
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///    The <see cref="UserManager{T}"/> where T is <see cref="User"/>.
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///    The <see cref="IMapper"/>.
        /// </summary>
        private readonly IMapper _mapper;
        /// <summary>
        ///     Create a new instance of <see cref="QuestionService"/> and 
        ///     inject <see cref="ILogService"/> and <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="log"> The <see cref="ILogService"/> </param>
        /// <param name="db"> The  <see cref="ApplicationDbContext"/> </param>
        /// <param name="userManager"> The <see cref="UserManager{T}"/> where T is <see cref="User"/> </param>
        public QuestionService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db,
            UserManager<User> userManager
        )
        {
            _db = db;
            _log = log;
            _userManager = userManager;
            _mapper = mapper;
        }
        #endregion

        #region Methods Implementation

        public async Task<CommonResponse<List<DtoGetQuestion>>> GetAllQuestions
        (
          Guid userId
        )
        {
            try
            {
                var questions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()))
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoGetQuestion>>(questions);

                return CommonResponse<List<DtoGetQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllQuestions", userId);

                return CommonResponse<List<DtoGetQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<List<DtoGetQuestion>>> GetAllNonDisabledQuestions
        (
          Guid userId
        )
        {
            try
            {
                var notDisabledQuestions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && !x.IsDeleted)
                                                   .ToListAsync();

                var dtoNotDisabledQuestions = _mapper.Map<List<DtoGetQuestion>>(notDisabledQuestions);

                return CommonResponse<List<DtoGetQuestion>>.Response($"Not disabled questions retrieved succsessfully, {dtoNotDisabledQuestions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoNotDisabledQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllNonDisabledQuestions", userId);

                return CommonResponse<List<DtoGetQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoGetQuestion>> GetQuestion
        (
            Guid userId,
            Guid qestionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(x => x.User)
                                                  .FirstOrDefaultAsync(q => q.Id.Equals(qestionId) && q.UserId.Equals(userId.ToString()));

                if (question is null)
                    return CommonResponse<DtoGetQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoQuestion = _mapper.Map<DtoGetQuestion>(question);

                return CommonResponse<DtoGetQuestion>.Response("Question retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetQuestion", userId);

                return CommonResponse<DtoGetQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoCreateQuestionReturn>> CreateQuestion
        (
            Guid userId,
            DtoCreateQuestion newQuestion
        )
        {
            try
            {
                if (newQuestion is null)
                    return CommonResponse<DtoCreateQuestionReturn>.Response("Question is empty!!", false, System.Net.HttpStatusCode.BadRequest, null);

                var question = _mapper.Map<Question>(newQuestion);
                question.UserId = userId.ToString();

                _db.Questions.Add(question);
                await _db.SaveChangesAsync();

                var dtoQuestion = _mapper.Map<DtoCreateQuestionReturn>(question);

                dtoQuestion.TagId = newQuestion.TagId;

                return CommonResponse<DtoCreateQuestionReturn>.Response("Question created succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateQuestion", userId);

                return CommonResponse<DtoCreateQuestionReturn>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoUpdateQuestion>> UpdateQuestion
        (
            Guid userId,
            DtoUpdateQuestion question
        )
        {
            try
            {
                var questionToBeUpdated = await _db.Questions.FirstOrDefaultAsync(q => q.Id.Equals(question.Id) && q.UserId.Equals(userId.ToString()));

                if (questionToBeUpdated is null)
                    return CommonResponse<DtoUpdateQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, question);

                Question newValuesOfQuestion = _mapper.Map<Question>(question);
                newValuesOfQuestion.UserId = userId.ToString();
                newValuesOfQuestion.CreatedAt = questionToBeUpdated.CreatedAt;

                _db.Entry(questionToBeUpdated).CurrentValues.SetValues(newValuesOfQuestion);

                _db.Questions.Update(questionToBeUpdated);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoUpdateQuestion>.Response("Question updated succsessfully", true, System.Net.HttpStatusCode.OK, question);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "UpdateQuestion", userId);

                return CommonResponse<DtoUpdateQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, question);
            }
        }

        public async Task<CommonResponse<DtoDisabledQuestion>> DisableQuestion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var questionToBeDisabled = await _db.Questions.FirstOrDefaultAsync(q => q.Id.Equals(questionId) && q.UserId.Equals(userId.ToString()));

                if (questionToBeDisabled is null)
                    return CommonResponse<DtoDisabledQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                if (questionToBeDisabled.IsDeleted)
                    return CommonResponse<DtoDisabledQuestion>.Response("This question is already disabled", false, System.Net.HttpStatusCode.BadRequest, null);

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(questionToBeDisabled);

                questionToBeDisabled.DeletedAt = DateTime.UtcNow;
                questionToBeDisabled.IsDeleted = true;

                _db.Questions.Update(questionToBeDisabled);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoDisabledQuestion>.Response("Question disabled succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DisableQuestion", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, new DtoDisabledQuestion());
            }
        }

        public async Task<CommonResponse<List<DtoDisabledQuestion>>> GetAllDisabledQuesions
        (
            Guid userId
        )
        {
            try
            {
                var questions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted)
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoDisabledQuestion>>(questions);

                return CommonResponse<List<DtoDisabledQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllDisabledQuesions", userId);

                return CommonResponse<List<DtoDisabledQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoDisabledQuestion>> GetDisabledQuesion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted && x.Id.Equals(questionId))
                                                   .FirstOrDefaultAsync();

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(question);

                return CommonResponse<DtoDisabledQuestion>.Response($"Question retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetDisabledQuesion", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoDeletedQuestion>> DeleteQuestion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var questionToBeDeleted = await _db.Questions.FirstOrDefaultAsync(q => q.Id.Equals(questionId) && q.UserId.Equals(userId.ToString()));

                var dtoQuestion = _mapper.Map<DtoDeletedQuestion>(questionToBeDeleted);

                if (questionToBeDeleted is null)
                    return CommonResponse<DtoDeletedQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                _db.Questions.Remove(questionToBeDeleted);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoDeletedQuestion>.Response("Question deleted succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteQuestion", userId);

                return CommonResponse<DtoDeletedQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        public async Task<CommonResponse<DtoDisabledQuestion>> UnDisableQuestion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var questionToBeUnDisabled = await _db.Questions.FirstOrDefaultAsync(q => q.Id.Equals(questionId) && q.UserId.Equals(userId.ToString()));

                if (questionToBeUnDisabled is null)
                    return CommonResponse<DtoDisabledQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                questionToBeUnDisabled.IsDeleted = false;
                questionToBeUnDisabled.EditedAt = DateTime.Now;

                _db.Questions.Update(questionToBeUnDisabled);
                await _db.SaveChangesAsync();

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(questionToBeUnDisabled);

                return CommonResponse<DtoDisabledQuestion>.Response("Question un disabled succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteQuestion", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }



        #endregion
    }
}