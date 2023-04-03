#region Usings
using FAQ.DAL.Models;
using FAQ.DAL.DataBase;
using FAQ.LOGGER.ServiceInterface;
#endregion

namespace FAQ.LOGGER.ServiceImplementation
{
    /// <summary>
    ///   A service class providing the logging by implemeting the ILogService interface.
    /// </summary>
    public class LogService : ILogService
    {
        #region Services innjection

        /// <summary>
        ///     Database context
        /// </summary>
        private readonly ApplicationDbContext _db;

        /// <summary>
        ///     Inject databaze in ctor.
        /// </summary>
        /// <param name="db"> Database object </param>
        public LogService(ApplicationDbContext db)
        {
            _db = db;
        }

        #endregion

        #region Methods implementation

        /// <summary>
        ///     Create log exception and save in the database method implementation
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="methodName">  Name of the method </param>
        /// <returns> Nothing </returns>
        public async Task CreateLogException(Exception ex, string methodName, Guid? UserId)
        {
            try
            {
                Log log = new()
                {
                    MethodName = methodName,
                    Description = ex.ToString(),
                    UserId = UserId,
                    LogTypeId = 1
                };

                _db.Logs.Add(log);
                await _db.SaveChangesAsync();

            }
            catch (Exception exception)
            {
                LogException(exception, methodName);
            }
        }

        /// <summary>
        ///     Create log user actions and save in the database method implementation
        /// </summary>
        /// <param name="description"> Description of the method </param>
        /// <param name="methodName"> Name of the method </param>
        /// <returns> Nothing </returns>
        public async Task CreateLogAction(string description, string methodName, Guid userId)
        {
            try
            {
                Log log = new()
                {
                    MethodName = methodName,
                    Description = description,
                    UserId = userId,
                    LogTypeId = 2
                };


                _db.Logs.Add(log);
                await _db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                LogException(ex, methodName);
            }
        }

        /// <summary>
        ///     Save the exeption in a  file
        /// </summary>
        /// <param name="ex"> The  exeption </param>
        private void LogException(Exception ex, string method)
        {
            //string projectDirectory = Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.FullName;
            string projectDirectory = System.IO.Path.GetDirectoryName(typeof(LogService).Assembly.Location)!;
            string logDirectory = Path.Combine(projectDirectory, "Logs");

            if (!Directory.Exists(logDirectory))
                Directory.CreateDirectory(logDirectory);

            string exceptionsDirectory = Path.Combine(logDirectory, "Exceptions");

            if (!Directory.Exists(exceptionsDirectory))
                Directory.CreateDirectory(exceptionsDirectory);

            string logFileName = $"exception_{DateTime.Now:yyyyMMdd}.log";
            string logFilePath = Path.Combine(exceptionsDirectory, logFileName);

            if (File.Exists(logFilePath))
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  ==> Method {method}");
                    sw.WriteLine($"Exception type: {ex.GetType().FullName}.. ==> Exception source: {ex.Source} ==> Exception message: {ex.Message}");
                    sw.WriteLine($"Stack trace: {ex.StackTrace}");

                    Exception innerException = ex.InnerException!;
                    int innerExceptionCount = 1;

                    while (innerException != null)
                    {
                        sw.WriteLine($"Inner exception {innerExceptionCount++}: ==> Exception type: {innerException.GetType().FullName} ==> Exception source: {innerException.Source} ==> Exception message: {innerException.Message}");
                        sw.WriteLine($"Stack trace: {innerException.StackTrace} \n");

                        innerException = innerException.InnerException!;
                    }

                    sw.WriteLine("\n");
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(logFilePath))
                {
                    sw.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}]  ==> Method {method}");
                    sw.WriteLine($"Exception type: {ex.GetType().FullName}.. ==> Exception source: {ex.Source} ==> Exception message: {ex.Message}");
                    sw.WriteLine($"Stack trace: {ex.StackTrace}");

                    Exception innerException = ex.InnerException!;
                    int innerExceptionCount = 1;

                    while (innerException != null)
                    {
                        sw.WriteLine($"Inner exception {innerExceptionCount++}: ==> Exception type: {innerException.GetType().FullName} ==> Exception source: {innerException.Source} ==> Exception message: {innerException.Message}");
                        sw.WriteLine($"Stack trace: {innerException.StackTrace} \n");

                        innerException = innerException.InnerException!;
                    }

                    sw.WriteLine("\n");
                }
            }
        }

        #endregion
    }
}