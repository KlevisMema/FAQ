#region Usings
using FAQ.DAL.Models;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.BLL.RepositoryService.Interfaces;
using FAQ.API.ControllerResponse;
using FAQ.DTO.QuestionsDtos;
#endregion

namespace FAQ.API.Controllers
{
    /// <summary>
    ///     A controller providing endpoints for question functionalities.
    /// </summary>
    public class QuestionsController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IQuestionService _questionService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionService"></param>
        public QuestionsController
        (
            IQuestionService questionService
        )
        {
            _questionService = questionService;
        }


        [HttpGet("GetAllQuestions/{userId}")]
        public async Task<ActionResult<CommonResponse<List<DtoGetQuestion>>>> GetAllQuestions
        (
            [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponseList(await _questionService.GetAllQuestions(userId));
        }

        [HttpGet("GetQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoGetQuestion>>> GetQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponse(await _questionService.GetQuestion(userId, questionId));
        }

        [HttpPost("CreateQuestion/{userId}")]
        public async Task<ActionResult<CommonResponse<DtoCreateQuestion>>> CreateQuestion
        (
            [FromRoute] Guid userId,
            [FromForm] DtoCreateQuestion question
        )
        {
            return StatusCodeResponse<DtoCreateQuestion>.ControllerResponse(await _questionService.CreateQuestion(userId, question));
        }

        [HttpPut("UpdateQuestion/{userId}")]
        public async Task<ActionResult<CommonResponse<DtoUpdateQuestion>>> UpdateQuestion
        (
            [FromRoute] Guid userId,
            [FromForm] DtoUpdateQuestion question
        )
        {
            return StatusCodeResponse<DtoUpdateQuestion>.ControllerResponse(await _questionService.UpdateQuestion(userId, question));
        }

        [HttpDelete("DeleteQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoDeletedQuestion>>> UpdateQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDeletedQuestion>.ControllerResponse(await _questionService.DeleteQuestion(userId, questionId));
        }
    }
}
