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
        private readonly IQuestionTagService _questionTagService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="questionService"></param>
        /// <param name="questionTagService"></param>
        public QuestionsController
        (
            IQuestionService questionService,
            IQuestionTagService questionTagService
        )
        {
            _questionService = questionService;
            _questionTagService = questionTagService;
        }


        [HttpGet("GetAllQuestions/{userId}")]
        public async Task<ActionResult<CommonResponse<List<DtoGetQuestion>>>> GetAllQuestions
        (
            [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponseList(await _questionService.GetAllQuestions(userId));
        }

        [HttpGet("GetAllNonDisabledQuestions/{userId}")]
        public async Task<ActionResult<CommonResponse<List<DtoGetQuestion>>>> GetAllNonDisabledQuestions
        (
            [FromRoute] Guid userId
        )
        {
            return StatusCodeResponse<DtoGetQuestion>.ControllerResponseList(await _questionService.GetAllNonDisabledQuestions(userId));
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

        [HttpGet("GetAllDisabledQuesions/{userId}")]
        public async Task<ActionResult<CommonResponse<List<DtoDisabledQuestion>>>> GetAllDisabledQuesions
       (
           [FromRoute] Guid userId
       )
        {
            return StatusCodeResponse<List<DtoDisabledQuestion>>.ControllerResponse(await _questionService.GetAllDisabledQuesions(userId));
        }

        [HttpGet("GetDisabledQuesion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>> GetDisabledQuesion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.GetDisabledQuesion(userId, questionId));
        }

        [HttpGet("GetQuestionWithAnswers/{userId}/{questionId}")]
        public async Task<ActionResult<DtoQuestionAnswers>> GetQuestionWithAnswers
       (
           [FromRoute] Guid userId,
           [FromRoute] Guid questionId
       )
        {
            return StatusCodeResponse<DtoQuestionAnswers>.ControllerResponse(await _questionService.GetQuestionWithAnswersAndChildAnswers(userId, questionId));
        }

        [HttpGet("GetQuestionWithAnswersNoChildAnswers/{userId}/{questionId}")]
        public async Task<ActionResult<DtoQuestionAnswers>> GetQuestionWithAnswersNoChildAnswers
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoQuestionAnswers>.ControllerResponse(await _questionService.GetQuestionWithAnswersNoChildAnswers(userId, questionId));
        }

        [HttpPost("CreateQuestion/{userId}")]
        public async Task<ActionResult<CommonResponse<DtoCreateQuestion>>> CreateQuestion
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

        [HttpPut("UpdateQuestion/{userId}")]
        public async Task<ActionResult<CommonResponse<DtoUpdateQuestion>>> UpdateQuestion
        (
            [FromRoute] Guid userId,
            [FromForm] DtoUpdateQuestion question
        )
        {
            return StatusCodeResponse<DtoUpdateQuestion>.ControllerResponse(await _questionService.UpdateQuestion(userId, question));
        }

        [HttpPut("DisableQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>> DisableQuestion
        (
           [FromRoute] Guid userId,
           [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.DisableQuestion(userId, questionId));
        }


        [HttpPut("UnDisableQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoDisabledQuestion>>> UnDisableQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDisabledQuestion>.ControllerResponse(await _questionService.UnDisableQuestion(userId, questionId));
        }

        [HttpDelete("DeleteQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<DtoDeletedQuestion>>> DeleteQuestion
        (
          [FromRoute] Guid userId,
          [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoDeletedQuestion>.ControllerResponse(await _questionService.DeleteQuestion(userId, questionId));
        }

    }
}
