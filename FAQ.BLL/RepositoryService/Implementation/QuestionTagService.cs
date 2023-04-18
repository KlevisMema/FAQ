#region Usings
using AutoMapper;
using FAQ.DTO.TagDtos;
using FAQ.DAL.DataBase;
using FAQ.SHARED.ResponseTypes;
using Microsoft.EntityFrameworkCore;
using FAQ.DAL.Models;
using FAQ.DTO.QuestionsDtos;
using FAQ.LOGGER.ServiceInterface;
using FAQ.BLL.RepositoryService.Interfaces;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    public class QuestionTagService : IQuestionTagService
    {
        #region Fields and Constructor
        /// <summary>
        /// 
        /// </summary>
        private readonly ApplicationDbContext _db;
        /// <summary>
        /// 
        /// </summary>
        private readonly IMapper _mapper;
        private readonly ILogService _log;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="db"></param>
        public QuestionTagService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db
        )
        {
            _db = db;
            _log = log;
            _mapper = mapper;
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public async Task<CommonResponse<List<DtoTag>>> GetTags
        //(
        //    Guid userId
        //)
        //{
        //    try
        //    {
        //        var tags = await _db.Tags.ToListAsync();

        //        var dtoTags = _mapper.Map<List<DtoTag>>(tags);

        //        return CommonResponse<List<DtoTag>>.Response("Tags retrieved successfully.", true, System.Net.HttpStatusCode.OK, dtoTags);
        //    }
        //    catch (Exception ex)
        //    {
        //        await _log.CreateLogException(ex, "GetTags", userId);

        //        return CommonResponse<List<DtoTag>>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
        //    }
        //}

        public async Task<CommonResponse<DtoCreateQuestion>> CreateQuestionTag
        (
           Guid userId,
           DtoCreateQuestionReturn dtoCreateQuestion
        )
        {
            try
            {
                var QuestionTag = new QuestionTag()
                {
                    QuestionId = dtoCreateQuestion.QuestionId,
                    TagId = dtoCreateQuestion.TagId
                };


                _db.QuestionTags.Add(QuestionTag);
                await _db.SaveChangesAsync();

                var dtoCreateQuestionReturn = _mapper.Map<DtoCreateQuestion>(QuestionTag);

                dtoCreateQuestionReturn.P_Question = dtoCreateQuestion.P_Question;
                dtoCreateQuestionReturn.Tittle = dtoCreateQuestion.Tittle;

                return CommonResponse<DtoCreateQuestion>.Response("Question created succsessfully", true, System.Net.HttpStatusCode.OK, dtoCreateQuestionReturn);
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateQuestionTag", userId);

                return CommonResponse<DtoCreateQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
    }
}