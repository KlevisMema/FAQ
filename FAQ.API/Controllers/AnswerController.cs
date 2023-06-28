#region Usings
using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.API.ControllerResponse;
using FAQ.BLL.RepositoryService.Interfaces;
using Microsoft.AspNetCore.Authorization;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A controller providing endpoints for answer functionalities.
    ///     This controller endpoints is protected with <see cref="AuthorizeAttribute"/>.
    /// </summary>
    [Authorize]
    public class AnswerController : BaseController
    {
        #region Properties/Fields
        /// <summary>
        ///     A  <see langword="private"/> readonly field for <see cref="IAnswerService"/>
        /// </summary>
        private readonly IAnswerService _answerService;
        #endregion

        #region Constructor
        /// <summary>
        ///     The <see cref="AnswerController"/> controller.
        ///     Injected <see cref="IAnswerService"/>.
        /// </summary>
        /// <param name="answerService"> The <see cref="IAnswerService"/></param>
        public AnswerController
        (
            IAnswerService answerService
        )
        {
            _answerService = answerService;
        }
        #endregion

        #region Methods / Endpoints
        /// <summary>
        ///     [GET] -
        ///     Get answers of an question.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="questionId"> The id of the question </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where 
        ///     T => <see cref="List{T}"/> where T => <see cref="DtoGetAnswer"/>.
        /// </returns>
        [HttpGet("GetAnswersOfQuestion/{userId}/{questionId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoGetAnswer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoGetAnswer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoGetAnswer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoGetAnswer>))]
        public async Task<ActionResult<CommonResponse<List<DtoGetAnswer>>>>
        GetAnswersOfQuestion
        (
           [FromRoute] Guid userId,
           [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoGetAnswer>.ControllerResponseList(await _answerService.GetAnswersOfQuestion(userId, questionId));
        }
        /// <summary>
        ///     [POST] -
        ///     Create an answer.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="dtoCreateAnswer"> The <see cref="DtoCreateAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateAnswer"/>
        /// </returns>
        [HttpPost("CreateAnswer/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoCreateAnswer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoCreateAnswer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoCreateAnswer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoCreateAnswer>))]
        public async Task<ActionResult<CommonResponse<DtoCreateAnswer>>>
        CreateAnswer
        (
            [FromRoute] Guid userId,
            [FromForm] DtoCreateAnswer dtoCreateAnswer
        )
        {
            return StatusCodeResponse<DtoCreateAnswer>.ControllerResponse(await _answerService.CreateAnswer(userId, dtoCreateAnswer));
        }
        /// <summary>
        ///     [POST] -
        ///     Create a answer for a answer.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerOfAnswer"> The <see cref="DtoAnswerOfAnswer"/> object </param>
        /// <returns></returns>
        [HttpPost("CreateAnswerOfAnAnswer/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoAnswerOfAnswer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoAnswerOfAnswer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoAnswerOfAnswer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoAnswerOfAnswer>))]
        public async Task<ActionResult<CommonResponse<DtoAnswerOfAnswer>>>
        CreateAnswerOfAnAnswer
        (
            [FromRoute] Guid userId,
            [FromForm] DtoAnswerOfAnswer answerOfAnswer
        )
        {
            return StatusCodeResponse<DtoAnswerOfAnswer>.ControllerResponse(await _answerService.CreateAnswerOfAnAnswer(userId, answerOfAnswer));
        }
        /// <summary>
        ///     [PUT] -
        ///     Edit an answer.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="editAnswer"> The <see cref="DtoEditAnswer"/> object </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoEditAnswer"/>
        /// </returns>
        [HttpPut("EditAnswer/{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoEditAnswer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoEditAnswer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoEditAnswer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoEditAnswer>))]
        public async Task<ActionResult<CommonResponse<DtoEditAnswer>>>
        EditAnswer
        (
            [FromRoute] Guid userId,
            [FromForm] DtoEditAnswer editAnswer
        )
        {
            return StatusCodeResponse<DtoEditAnswer>.ControllerResponse(await _answerService.EditAnswer(userId, editAnswer));
        }
        /// <summary>
        ///     [DELETE] -
        ///     Delete an answer.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="answerId"> The id of the answer</param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoDeleteAnswer"/>.
        /// </returns>
        [HttpDelete("DeleteAnswer/{userId}/{answerId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CommonResponse<DtoDeleteAnswer>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(CommonResponse<DtoDeleteAnswer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(CommonResponse<DtoDeleteAnswer>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(CommonResponse<DtoDeleteAnswer>))]
        public async Task<ActionResult<CommonResponse<DtoDeleteAnswer>>>
        DeleteAnswer
        (
            [FromRoute] Guid userId,
            Guid answerId
        )
        {
            return StatusCodeResponse<DtoDeleteAnswer>.ControllerResponse(await _answerService.DeleteAnswer(userId, answerId));
        }
        #endregion
    }
}