#region Usings
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.SHARED.ResponseTypes;
using FAQ.LOGGER.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using FAQ.BLL.RepositoryService.Interfaces;
using Microsoft.AspNetCore.Identity;
using FAQ.DTO.QuestionsDtos;
using AutoMapper;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    public class QuestionService : IQuestionService
    {

        #region Services Injection
        /// <summary>
        ///     Log service
        /// </summary>
        private readonly ILogService _log;
        /// <summary>
        ///     Database Context
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        ///     User Manager service
        /// </summary>
        private readonly UserManager<User> _userManager;
        /// <summary>
        ///    A readonly field for Mapper service
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
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                    return CommonResponse<List<DtoGetQuestion>>.Response("User doesn't exists", false, System.Net.HttpStatusCode.NotFound, Enumerable.Empty<DtoGetQuestion>().ToList());

                var questions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()))
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoGetQuestion>>(questions);

                return CommonResponse<List<DtoGetQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllQuestions", userId);

                return CommonResponse<List<DtoGetQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, Enumerable.Empty<DtoGetQuestion>().ToList());
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
                    return CommonResponse<DtoGetQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, new DtoGetQuestion());

                var dtoQuestion = _mapper.Map<DtoGetQuestion>(question);

                return CommonResponse<DtoGetQuestion>.Response("Question retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetQuestion", userId);

                return CommonResponse<DtoGetQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, new DtoGetQuestion());
            }
        }

        public async Task<CommonResponse<DtoCreateQuestion>> CreateQuestion
        (
            Guid userId,
            DtoCreateQuestion newQuestion
        )
        {
            try
            {
                if (newQuestion is null)
                    return CommonResponse<DtoCreateQuestion>.Response("Question is empty!!", false, System.Net.HttpStatusCode.BadRequest, newQuestion);

                var question = _mapper.Map<Question>(newQuestion);
                question.UserId = userId.ToString();

                _db.Questions.Add(question);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoCreateQuestion>.Response("Question created succsessfully", true, System.Net.HttpStatusCode.OK, newQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateQuestion", userId);

                return CommonResponse<DtoCreateQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, newQuestion);
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
                if (question is null)
                    return CommonResponse<DtoUpdateQuestion>.Response("Question is empty!!", false, System.Net.HttpStatusCode.BadRequest, question);

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

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(questionToBeDisabled);

                if (questionToBeDisabled is null)
                    return CommonResponse<DtoDisabledQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, dtoQuestion);

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
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                    return CommonResponse<List<DtoDisabledQuestion>>.Response("User doesn't exists", false, System.Net.HttpStatusCode.NotFound, Enumerable.Empty<DtoDisabledQuestion>().ToList());

                var questions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted)
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoDisabledQuestion>>(questions);

                return CommonResponse<List<DtoDisabledQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllDisabledQuesions", userId);

                return CommonResponse<List<DtoDisabledQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, Enumerable.Empty<DtoDisabledQuestion>().ToList());
            }
        }

        public async Task<CommonResponse<DtoDisabledQuestion>> GetDisabledQuesions
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());

                if (user is null)
                    return CommonResponse<DtoDisabledQuestion>.Response("User doesn't exists", false, System.Net.HttpStatusCode.NotFound, new DtoDisabledQuestion());

                var questions = await _db.Questions.Include(user => user.User)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted && x.Id.Equals(questionId))
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<DtoDisabledQuestion>(questions);

                return CommonResponse<DtoDisabledQuestion>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllDisabledQuesions", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError,new DtoDisabledQuestion());
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
                    return CommonResponse<DtoDeletedQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, dtoQuestion);

                _db.Questions.Remove(questionToBeDeleted);
                await _db.SaveChangesAsync();

                return CommonResponse<DtoDeletedQuestion>.Response("Question deleted succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteQuestion", userId);

                return CommonResponse<DtoDeletedQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, new DtoDeletedQuestion());
            }
        }
        #endregion
    }
}