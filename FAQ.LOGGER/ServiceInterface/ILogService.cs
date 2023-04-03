namespace FAQ.LOGGER.ServiceInterface
{
    /// <summary>
    ///     A service interface providing the logging
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        ///     Create log and save in the database method declaration
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <param name="methodName">  Name of the method </param>
        /// <returns> Nothing </returns>
        Task CreateLogException(Exception ex, string methodName, Guid? UserId);

        /// <summary>
        ///     Create log user actions and save in the database method declaration
        /// </summary>
        /// <param name="description"> Description of the method </param>
        /// <param name="methodName"> Name of the method </param>
        /// <returns> Nothing </returns>
        Task CreateLogAction(string description, string methodName, Guid userId);
    }
}