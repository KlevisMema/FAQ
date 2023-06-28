#region Usings
using AutoMapper;
using FAQ.DAL.DataBase;
using FAQ.LOGGER.ServiceInterface;
#endregion

namespace FAQ.BLL.RepositoryService.BaseServices
{
    /// <summary>
    ///     A base class that provides some common shared services.
    /// </summary>
    public class CommonServices
    {
        #region Fields and Constructor
        /// <summary>
        ///     The <see cref="ApplicationDbContext"/>.
        /// </summary>
        protected readonly ApplicationDbContext _db;
        /// <summary>
        ///     The <see cref="IMapper"/>.
        /// </summary>
        protected readonly IMapper _mapper;
        /// <summary>
        ///     The <see cref="ILogService"/>.
        /// </summary>
        protected readonly ILogService _log;
        /// <summary>
        ///     Inject services and create a new instance of  <see cref="CommonServices"/> 
        /// </summary>
        /// <param name="mapper"> The <see cref="IMapper"/> </param>
        /// <param name="db"> The <see cref="ApplicationDbContext"/> </param>
        /// <param name="log"> The <see cref="ILogService"/> </param>
        public CommonServices
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
    }
}
