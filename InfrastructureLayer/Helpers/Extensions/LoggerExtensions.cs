using Microsoft.Extensions.Logging;
using System;

namespace InfrastructureLayer.Helpers.Extensions
{
    /// <summary>
    /// Extends the ILogger interface.
    /// </summary>
    public static class LoggerExtensions
    {
        /// <summary>
        /// Log an exception as error and include all inner exceptions.
        /// </summary>
        public static void LogErrorWithInnerExceptions(this ILogger logger, Exception ex)
        {
            logger.LogError(ex.Message, ex);

            var iex = ex.InnerException;
            while (iex != null)
            {
                logger.LogError($"Inner exception message: {iex.Message}", iex);
                iex = iex.InnerException;
            }
        }
    }
}