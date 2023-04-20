﻿using FAQ.DTO.AnswerDtos;
using FAQ.SHARED.ResponseTypes;
using Microsoft.AspNetCore.Mvc;
using FAQ.API.ControllerResponse;
using FAQ.BLL.RepositoryService.Interfaces;

namespace FAQ.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AnswerController : BaseController
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IAnswerService _answerService;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="answerService"></param>
        public AnswerController
        (
            IAnswerService answerService
        )
        {
            _answerService = answerService;
        }

        [HttpGet("GetAnswersOfQuestion/{userId}/{questionId}")]
        public async Task<ActionResult<CommonResponse<List<DtoGetAnswer>>>> GetAnswersOfQuestion
        (
            [FromRoute] Guid userId,
            [FromRoute] Guid questionId
        )
        {
            return StatusCodeResponse<DtoGetAnswer>.ControllerResponseList(await _answerService.GetAnswersOfQuestion(userId, questionId));
        }

        [HttpPost("CreateAnswer/{userId}")]
        public async Task<ActionResult<CommonResponse<DtoCreateAnswer>>> CreateAnswer
        (
            [FromRoute] Guid userId,
            [FromForm] DtoCreateAnswer dtoCreateAnswer
        )
        {
            return StatusCodeResponse<DtoCreateAnswer>.ControllerResponse(await _answerService.CreateAnswer(userId, dtoCreateAnswer));
        }
    }
}