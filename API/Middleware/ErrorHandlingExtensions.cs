using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;

namespace API.Middleware
{
    /// <summary>
    /// Extends the error handling class.
    /// </summary>
    public static class ErrorHandlingExtensions
    {
        /// <summary>
        /// Use the ErrorHandling middleware.
        /// </summary>
        public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder, ILogger logger)
        {
            return builder.UseMiddleware<ErrorHandling>(logger);
        }
    }
}