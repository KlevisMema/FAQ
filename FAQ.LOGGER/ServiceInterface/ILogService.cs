namespace FAQ.LOGGER.ServiceInterface
{
    /// <summary>
    ///     A service interface providing logging.
    /// </summary>
    public interface ILogService
    {
        #region Methods Declaration

        /// <summary>
        ///     Create log exception and save in the database, method declaration.
        /// </summary>
        /// <param name="ex"> Exception object of type <see cref="Exception"/> </param>
        /// <param name="methodName"> Name of the method  of type <see cref="string"/> </param>
        /// <param name="UserId"> Id of the user value of type <see cref="Guid"/> </param>
        /// <returns> Nothing </returns>
        Task CreateLogException
        (
            Exception ex,
            string methodName,
            Guid? UserId
        );
        /// <summary>
        ///     Create log user actions and save in the database, method declaration.
        /// </summary>
        /// <param name="description"> Description of the action value of type <see cref="string"/> </param>
        /// <param name="methodName"> Name of the method  of type <see cref="string"/> </param>
        /// <param name="userId"> Id of the user value of type <see cref="Guid"/> </param>
        /// <returns> Nothing </returns>
        Task CreateLogAction
        (
            string description,
            string methodName,
            Guid userId
        ); 

        #endregion
    }
}