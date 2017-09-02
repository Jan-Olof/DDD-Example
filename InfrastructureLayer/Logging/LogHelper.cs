using Microsoft.Extensions.Logging;
using System;

namespace InfrastructureLayer.Logging
{
    public static class LogHelper
    {
        /// <summary>
        /// Log an exception and include all inner exception.
        /// </summary>
        public static void LogExceptionWithInnerExceptions(Exception ex, ILogger logger)
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