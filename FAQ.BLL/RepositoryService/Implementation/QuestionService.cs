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
using FAQ.EMAIL.EmailService.ServiceInterface;
using FAQ.BLL.RepositoryService.BaseServices;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    /// <summary>
    ///     A Service class that proviced functionalities for
    ///     questions by implementing the <see cref="IQuestionService"/> interface.
    /// </summary>
    public class QuestionService : CommonServices, IQuestionService
    {
        #region Properties / Constructor / Injections
        /// <summary>
        ///     The <see cref="IEmailSender"/>
        /// </summary>
        private readonly IEmailSender _emailSender;
        /// <summary>
        ///     Create a new instance of <see cref="QuestionService"/> and 
        ///     inject <see cref="ILogService"/> and <see cref="ApplicationDbContext"/>.
        /// </summary>
        /// <param name="log"> The <see cref="ILogService"/> </param>
        /// <param name="db"> The  <see cref="ApplicationDbContext"/> </param>
        /// <param name="mapper"> the <see cref="IMapper"/> </param>
        public QuestionService
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

        #region Methods Implementation

        /// <summary>
        ///     Get all questions from questions table
        ///     method implementation.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id. </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="List{T}"/> and T is <see cref="DtoGetQuestion"/>. 
        /// </returns>
        public async Task<CommonResponse<List<DtoGetQuestion>>> GetAllQuestions
        (
            Guid userId
        )
        {
            try
            {
                var questions = await _db.Questions.Include(user => user.User!)
                                                   .Include(x => x.QuestionTags!)
                                                   .ThenInclude(x => x.Tag)
                                                   .Where(x => x.UserId.Equals(userId.ToString()))
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoGetQuestion>>(questions);

                return CommonResponse<List<DtoGetQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllQuestions", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<List<DtoGetQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Get all questions from questions table
        ///     method implementation that are not disabled.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id. </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="List{T}"/> and T is <see cref="DtoGetQuestion"/>. 
        /// </returns>
        public async Task<CommonResponse<List<DtoGetQuestion>>> GetAllNonDisabledQuestions
        (
          Guid userId
        )
        {
            try
            {
                var notDisabledQuestions = await _db.Questions.Include(user => user.User)
                                                   .Include(x => x.QuestionTags!)
                                                   .ThenInclude(x => x.Tag)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && !x.IsDeleted)
                                                   .ToListAsync();

                var dtoNotDisabledQuestions = _mapper.Map<List<DtoGetQuestion>>(notDisabledQuestions);

                return CommonResponse<List<DtoGetQuestion>>.Response($"Not disabled questions retrieved succsessfully, {dtoNotDisabledQuestions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoNotDisabledQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllNonDisabledQuestions", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<List<DtoGetQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Get a question from questions table
        ///     method implementation.
        /// </summary>
        /// <param name="userId"> The <see cref="Guid"/> user id </param>
        /// <returns> 
        ///     <see cref="Task{TResult}"/> where TResult is <see cref="CommonResponse{T}"/> 
        ///     where T is <see cref="DtoGetQuestion"/>.
        /// </returns>
        public async Task<CommonResponse<DtoGetQuestion>>
        GetQuestion
        (
            Guid userId,
            Guid qestionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(x => x.User)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .FirstOrDefaultAsync(q => q.Id.Equals(qestionId) && q.UserId.Equals(userId.ToString()));

                if (question is null)
                    return CommonResponse<DtoGetQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoQuestion = _mapper.Map<DtoGetQuestion>(question);

                return CommonResponse<DtoGetQuestion>.Response("Question retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoGetQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Create a question for a user method implementation.
        /// </summary>
        /// <param name="userId"> The user Id </param>
        /// <param name="newQuestion"> The <see cref="DtoCreateQuestion"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateQuestionReturn"/>
        /// </returns>
        public async Task<CommonResponse<DtoCreateQuestionReturn>>
        CreateQuestion
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

                await _log.CreateLogAction($"User with id {userId} updated the question with id  {question.Id}", "CreateQuestion", userId);

                return CommonResponse<DtoCreateQuestionReturn>.Response("Question created succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoCreateQuestionReturn>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Update a question of a user method implementation.
        /// </summary>
        /// <param name="userId"> The user Id </param>
        /// <param name="question"> The <see cref="DtoUpdateQuestion"/> </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoUpdateQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoUpdateQuestion>>
        UpdateQuestion
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

                await _log.CreateLogAction($"User with id {userId} updated the question with id  {question.Id}", "UpdateQuestion", userId);

                return CommonResponse<DtoUpdateQuestion>.Response("Question updated succsessfully", true, System.Net.HttpStatusCode.OK, question);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "UpdateQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoUpdateQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, question);
            }
        }
        /// <summary>
        ///     Disable a question of a user method implementation
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <param name="questionId"> The question id</param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoDisabledQuestion>>
        DisableQuestion
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


                questionToBeDisabled.DeletedAt = DateTime.UtcNow;
                questionToBeDisabled.IsDeleted = true;

                _db.Questions.Update(questionToBeDisabled);
                await _db.SaveChangesAsync();

                var question = await _db.Questions.Include(user => user.User)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted && x.Id.Equals(questionId))
                                                  .FirstOrDefaultAsync();

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(question);

                await _log.CreateLogAction($"User with id {userId} disabled the question with id  {question!.Id}", "DisableQuestion", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Question disabled succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);

            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DisableQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, new DtoDisabledQuestion());
            }
        }
        /// <summary>
        ///     Get all the disabled question of a user method implementation.
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/> and 
        ///     T is <see cref="DtoDisabledQuestion"/>
        /// </returns>
        public async Task<CommonResponse<List<DtoDisabledQuestion>>>
        GetAllDisabledQuesions
        (
            Guid userId
        )
        {
            try
            {
                var questions = await _db.Questions.Include(user => user.User)
                                                   .Include(x => x.QuestionTags!)
                                                   .ThenInclude(x => x.Tag)
                                                   .Where(x => x.UserId.Equals(userId.ToString()) && x.IsDeleted)
                                                   .ToListAsync();

                var dtoQuestions = _mapper.Map<List<DtoDisabledQuestion>>(questions);

                return CommonResponse<List<DtoDisabledQuestion>>.Response($"Questions retrieved succsessfully, {questions.Count} questions", true, System.Net.HttpStatusCode.OK, dtoQuestions);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetAllDisabledQuesions", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<List<DtoDisabledQuestion>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Get all disabled quesitons of a user method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoDisabledQuestion>> 
        GetDisabledQuesion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(user => user.User)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .Where(x => x.UserId.Equals(userId.ToString()) && x.Id.Equals(questionId))
                                                  .FirstOrDefaultAsync();

                if (question is null)
                    return CommonResponse<DtoDisabledQuestion>.Response($"Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                if (!question.IsDeleted)
                    return CommonResponse<DtoDisabledQuestion>.Response($"Question it's not disabled", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(question);

                return CommonResponse<DtoDisabledQuestion>.Response($"Question retrieved succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetDisabledQuesion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Delete a question of a user method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The question id </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeletedQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoDeletedQuestion>> 
        DeleteQuestion
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var questionToBeDeleted = await _db.Questions.Include(x => x.Answers)
                                                             .FirstOrDefaultAsync(q => q.Id.Equals(questionId) && q.UserId.Equals(userId.ToString()));

                if (questionToBeDeleted is null)
                    return CommonResponse<DtoDeletedQuestion>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                if (questionToBeDeleted.Answers is not null)
                {
                    foreach (var item in questionToBeDeleted.Answers)
                    {
                        _db.Answers.Remove(item);
                        await _db.SaveChangesAsync();
                    }
                }

                _db.Questions.Remove(questionToBeDeleted);
                await _db.SaveChangesAsync();

                await _log.CreateLogAction($"User with id {userId} deleted the question with id  {questionId}", "DeleteQuestion", userId);

                return CommonResponse<DtoDeletedQuestion>.Response("Question deleted succsessfully", true, System.Net.HttpStatusCode.OK, null);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoDeletedQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Un disable a question of a user method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoDisabledQuestion>> 
        UnDisableQuestion
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

                if (!questionToBeUnDisabled.IsDeleted)
                    return CommonResponse<DtoDisabledQuestion>.Response("This question is undisabled", false, System.Net.HttpStatusCode.BadRequest, null);

                questionToBeUnDisabled.IsDeleted = false;
                questionToBeUnDisabled.EditedAt = DateTime.Now;

                _db.Questions.Update(questionToBeUnDisabled);
                await _db.SaveChangesAsync();

                var question = await _db.Questions.Include(user => user.User)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .Where(x => x.UserId.Equals(userId.ToString()) && x.Id.Equals(questionId))
                                                  .FirstOrDefaultAsync();

                var dtoQuestion = _mapper.Map<DtoDisabledQuestion>(question);

                await _log.CreateLogAction($"User with id {userId} undisabled the question with id  {question!.Id}", "UnDisableQuestion", userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Question undisabled succsessfully", true, System.Net.HttpStatusCode.OK, dtoQuestion);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "DeleteQuestion", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoDisabledQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Get question with answers and answers with answers method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>
        /// </returns>
        public async Task<CommonResponse<DtoQuestionAnswers>> 
        GetQuestionWithAnswersAndChildAnswers
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(user => user.User)
                                                  .Include(x => x.Answers!)
                                                  .ThenInclude(x => x.ChildAnswers)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .Where(x => x.UserId.Equals(userId.ToString()) && x.Id.Equals(questionId))
                                                  .FirstOrDefaultAsync();

                if (question is null)
                    return CommonResponse<DtoQuestionAnswers>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoQuestionAnswers = _mapper.Map<DtoQuestionAnswers>(question);

                return CommonResponse<DtoQuestionAnswers>.Response($"Question retrieved succsessfully with answers ", true, System.Net.HttpStatusCode.OK, dtoQuestionAnswers);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetQuestionWithAnswersAndChildAnswers", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoQuestionAnswers>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Get questions with answers method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>
        /// </returns>
        public async Task<CommonResponse<DtoQuestionAnswers>> 
        GetQuestionWithAnswersNoChildAnswers
        (
            Guid userId,
            Guid questionId
        )
        {
            try
            {
                var question = await _db.Questions.Include(user => user.User)
                                                  .Include(x => x.Answers!)
                                                  .Include(x => x.QuestionTags!)
                                                  .ThenInclude(x => x.Tag)
                                                  .Where(x => x.UserId.Equals(userId.ToString()) && x.Id.Equals(questionId))
                                                  .FirstOrDefaultAsync();

                if (question is null)
                    return CommonResponse<DtoQuestionAnswers>.Response("Question doesn't exists", false, System.Net.HttpStatusCode.NotFound, null);

                var dtoQuestionAnswers = _mapper.Map<DtoQuestionAnswers>(question);

                return CommonResponse<DtoQuestionAnswers>.Response($"Question retrieved succsessfully with answers ", true, System.Net.HttpStatusCode.OK, dtoQuestionAnswers);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "GetQuestionWithAnswersNoChildAnswers", userId);

                await _emailSender.SendEmailToDevTeam(userId);

                return CommonResponse<DtoQuestionAnswers>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }

        #endregion
    }
}