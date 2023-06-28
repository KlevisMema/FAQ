#region Usings
using AutoMapper;
using FAQ.DAL.DataBase;
using FAQ.SHARED.ResponseTypes;
using FAQ.DAL.Models;
using FAQ.DTO.QuestionsDtos;
using FAQ.LOGGER.ServiceInterface;
using FAQ.BLL.RepositoryService.Interfaces;
using FAQ.BLL.RepositoryService.BaseServices;
#endregion

namespace FAQ.BLL.RepositoryService.Implementation
{
    /// <summary>
    ///     A service class that hold the buisness logic of 
    ///     the question tag and implements <see cref="IQuestionTagService"/>.
    /// </summary>
    public class QuestionTagService : CommonServices, IQuestionTagService
    {
        #region Properties / Constructor / Injections
        /// <summary>
        ///     Inject services in the 
        ///     <see cref="QuestionTagService"/> 
        ///     controller.
        /// </summary>
        /// <param name="mapper"> The <see cref="IMapper"/> </param>
        /// <param name="db"> The <see cref="ApplicationDbContext"/> </param>
        /// <param name="log"> The <see cref="ILogService"/> </param>
        public QuestionTagService
        (
            IMapper mapper,
            ILogService log,
            ApplicationDbContext db
        ) : base(mapper, log, db)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        ///     Create a question tag method implementation.
        /// </summary>
        /// <param name="userId"> The id of the user </param>
        /// <param name="dtoCreateQuestion"> 
        ///     The <see cref="DtoCreateQuestionReturn"/> object 
        /// </param>
        /// <returns>
        ///     <see cref="CommonResponse{T}"/> where T => <see cref="DtoCreateQuestion"/>
        /// </returns>
        public async Task<CommonResponse<DtoCreateQuestion>>
        CreateQuestionTag
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

                return CommonResponse<DtoCreateQuestion>.Response("Question created succsessfully", true, System.Net.HttpStatusCode.OK, Return_MapedObject(QuestionTag, dtoCreateQuestion));
            }
            catch (Exception ex)
            {
                await _log.CreateLogException(ex, "CreateQuestionTag", userId);

                return CommonResponse<DtoCreateQuestion>.Response("Internal server error!", false, System.Net.HttpStatusCode.InternalServerError, null);
            }
        }
        /// <summary>
        ///     Perform mapping from <see cref="QuestionTag"/> type to <see cref="DtoCreateQuestionReturn"/>
        /// </summary>
        /// <param name="QuestionTag"> The <see cref="QuestionTag"/> object </param>
        /// <param name="dtoCreateQuestion"> The <see cref="DtoCreateQuestionReturn"/> object </param>
        /// <returns></returns>
        private DtoCreateQuestion
        Return_MapedObject
        (
            QuestionTag QuestionTag,
            DtoCreateQuestionReturn dtoCreateQuestion
        )
        {
            var dtoCreateQuestionReturn = _mapper.Map<DtoCreateQuestion>(QuestionTag);

            dtoCreateQuestionReturn.P_Question = dtoCreateQuestion.P_Question;
            dtoCreateQuestionReturn.Tittle = dtoCreateQuestion.Tittle;

            return dtoCreateQuestionReturn;
        }
        #endregion
    }
}