namespace Helpers.Extensions
{
    /// <summary>
    /// Extensions to the string class.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Converts the string to an integer. Returns a tuple with value and success.
        /// </summary>
        public static (bool isSuccess, int parsedValue) Parser(this string str)
        {
            bool isSuccess = int.TryParse(str, out int parsedValue);

            return (isSuccess, parsedValue);
        }

        public static string ToSentenceCase(this string str) => $"{str.ToUpper()[0]}{str.ToLower().Substring(1)}";
    }
}