#region Usings
using FAQ.DAL.Models;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.BLL.RepositoryService.Interfaces;
using FAQ.API.ControllerResponse;
using FAQ.DTO.QuestionsDtos;
using Microsoft.AspNetCore.Authorization;
using FAQ.SECURITY.ApplicationAuthorizationService.ServiceInterface;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A controller providing endpoints for question functionalities.
    ///     This controller endpoints is protected with <see cref="AuthorizeAttribute"/>.
    /// </summary>
    [Authorize]
    public class QuestionsController : BaseController
    {
        #region Fields
        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IQuestionService"/>
        /// </summary>
        private readonly IQuestionService _questionService;
        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IQuestionTagService"/>
        /// </summary>
        private readonly IQuestionTagService _questionTagService;
        #endregion

        #region Constructor
        /// <summary>
        ///     The <see cref="QuestionsController"/> controller.
        ///     Injected <see cref="IQuestionService"/> and <see cref="IQuestionTagService"/>.
        /// </summary>
        /// <param name="questionService"> The <see cref="IQuestionService"/> </param>
        /// <param name="questionTagService"> The <see cref="IQuestionTagService"/> </param>
        public QuestionsController
        (
            IQuestionService questionService,
            IQuestionTagService questionTagService
        )
        {
            _questionService = questionService;
            _questionTagService = questionTagService;
        }
        #endregion

        #region Methods / Endpoints
        /// <summary>
        ///     [GET] -
        ///     Get all questions of a user ednpoint by <paramref name="userId"/> id of the user.
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <returns> 
        ///     <see cref="CommonResponse{T}"/> where T is <see cref="List{T}"/> T => <see cref="DtoGetQuestion"/> 
        /// </returns>
        [HttpGet("GetAllQuestions/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        public async Task<ActionResult<CommonResponse<List<DtoGetQuestion>>>>
        GetAllQuestions
        (
            [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponseList(await _questionService.GetAllQuestions(userId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get all non disabled questions.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/> T => <see cref="DtoGetQuestion"/>
        /// </returns>
        [HttpGet("GetAllNonDisabledQuestions/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<List<DtoGetQuestion>>))]
        public async Task<ActionResult<CommonResponse<List<DtoGetQuestion>>>>
        GetAllNonDisabledQuestions
        (
            [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponseList(await _questionService.GetAllNonDisabledQuestions(userId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get a question by id.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question  </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoGetQuestion"/>
        /// </returns>
        [HttpGet("GetQuestion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoGetQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoGetQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoGetQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoGetQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoGetQuestion>>>
        GetQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponse(await _questionService.GetQuestion(userId, questionId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get all disabled questions.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="List{T}"/> T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        [HttpGet("GetAllDisabledQuesions/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<List<DtoDisabledQuestion>>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<List<DtoDisabledQuestion>>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<List<DtoDisabledQuestion>>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<List<DtoDisabledQuestion>>))]
        public async Task<ActionResult<CommonResponse<List<DtoDisabledQuestion>>>>
        GetAllDisabledQuesions
        (
           [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<List<DtoDisabledQuestion>>.ControllerResponse(await _questionService.GetAllDisabledQuesions(userId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get disabled question by id.
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <param name="questionId"> The question if </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        [HttpGet("GetDisabledQuesion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>>
        GetDisabledQuesion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.GetDisabledQuesion(userId, questionId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get questions answers with answers.
        /// </summary>
        /// <param name="userId"> The user id </param>
        /// <param name="questionId"> The quesiton id </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>.
        /// </returns>
        [HttpGet("GetQuestionWithAnswers/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        public async Task<ActionResult<CommonResponse<DtoQuestionAnswers>>>
        GetQuestionWithAnswers
        (
           [FromRoute] Guid userId,
           [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoQuestionAnswers>.ControllerResponse(await _questionService.GetQuestionWithAnswersAndChildAnswers(userId, questionId));
        }
        /// <summary>
        ///     [GET] -
        ///     Get questions with answers.
        /// </summary>  
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the user</param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoQuestionAnswers"/>.
        /// </returns>
        [HttpGet("GetQuestionWithAnswersNoChildAnswers/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoQuestionAnswers>))]
        public async Task<ActionResult<CommonResponse<DtoQuestionAnswers>>>
        GetQuestionWithAnswersNoChildAnswers
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoQuestionAnswers>.ControllerResponse(await _questionService.GetQuestionWithAnswersNoChildAnswers(userId, questionId));
        }
        /// <summary>
        ///     [POST] -
        ///     Create a question.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="question"> The <see cref="DtoCreateQuestion"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateQuestion"/>
        /// </returns>
        [HttpPost("CreateQuestion/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoCreateQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoCreateQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoCreateQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoCreateQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoCreateQuestion>>>
        CreateQuestion
        (
            [FromRoute] Guid userId,
            [FromForm] DtoCreateQuestion question
        )
        {
            var createQuestionResult = await _questionService.CreateQuestion(userId, question);

            if (!createQuestionResult.Succsess && createQuestionResult.Value is null)
            {
                return StatusCodeResponse<DtoCreateQuestion>.ControllerResponse(new CommonResponse<DtoCreateQuestion>()
                {
                    StatusCode = createQuestionResult.StatusCode,
                    Succsess = createQuestionResult.Succsess,
                    Value = null,
                    Message = createQuestionResult.Message
                });
            }

            var createQuestionTagResult = await _questionTagService.CreateQuestionTag(userId, createQuestionResult.Value!);

            return StatusCodeResponse<DtoCreateQuestion>.ControllerResponse(createQuestionTagResult);
        }
        /// <summary>
        ///     [PUT] -
        ///     Update a question.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="question"> The <see cref="DtoUpdateQuestion"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoUpdateQuestion"/>
        /// </returns>
        [HttpPut("UpdateQuestion/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoUpdateQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoUpdateQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoUpdateQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoUpdateQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoUpdateQuestion>>>
        UpdateQuestion
        (
            [FromRoute] Guid userId,
            [FromForm] DtoUpdateQuestion question
        )
        {
            return StatusCodeResponse<DtoUpdateQuestion>.ControllerResponse(await _questionService.UpdateQuestion(userId, question));
        }
        /// <summary>
        ///     [PUT] -
        ///     Disable a question.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/> 
        /// </returns>
        [HttpPut("DisableQuestion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>>
        DisableQuestion
        (
           [FromRoute] Guid userId,
           [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.DisableQuestion(userId, questionId));
        }
        /// <summary>
        ///     [PUT] - 
        ///     Un disable a question.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDisabledQuestion"/>
        /// </returns>
        [HttpPut("UnDisableQuestion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoDisabledQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>>
        UnDisableQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.UnDisableQuestion(userId, questionId));
        }
        /// <summary>
        ///     [DELETE] - 
        ///     Delete a question of a user by id : <paramref name="questionId"/>.
        /// </summary>
        /// <param name="userId"> The id of user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeletedQuestion"/>
        /// </returns>
        [HttpDelete("DeleteQuestion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoDeletedQuestion>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoDeletedQuestion>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoDeletedQuestion>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoDeletedQuestion>))]
        public async Task<ActionResult<CommonResponse<DtoDeletedQuestion>>>
        DeleteQuestion
        (
          [FromRoute] Guid userId,
          [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDeletedQuestion>.ControllerResponse(await _questionService.DeleteQuestion(userId, questionId));
        }
        #endregion

    }
}
