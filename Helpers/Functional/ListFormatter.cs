using Helpers.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace Helpers.Functional
{
    public static class ListFormatter
    {
        /// <summary>
        /// Format as a numbered list in parallel.
        /// </summary>
        public static List<string> FormatParallel(List<string> list)
            => list
                .AsParallel()
                .Select(StringExtensions.ToSentenceCase)
                .Zip(Enumerable.Range(1, list.Count).AsParallel(), (s, i) => $"{i}. {s}")
                .ToList();

        /// <summary>
        /// Format as a numbered list.
        /// </summary>
        public static List<string> FormatSequential(List<string> list)
        {
            var left = list.Select(StringExtensions.ToSentenceCase);
            var right = Enumerable.Range(1, list.Count);
            var zipped = Enumerable.Zip(left, right, (s, i) => $"{i}. {s}");
            return zipped.ToList();
        }
    }
}