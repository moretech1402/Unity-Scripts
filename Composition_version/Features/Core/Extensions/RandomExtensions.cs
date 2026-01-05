using System;
using System.Collections.Generic;

namespace MC.Core.Extensions
{
    public static class RandomExtensions
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Returns a random element from the list.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the collection is empty.
        /// </exception>
        public static T RandomElement<T>(this IReadOnlyList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list.Count == 0)
                throw new InvalidOperationException("Cannot select a random element from an empty collection.");

            return list[_random.Next(list.Count)];
        }

        /// <summary>
        /// Returns a random element from the enumerable.
        /// </summary>
        public static T RandomElement<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (source is IReadOnlyList<T> list)
                return list.RandomElement();

            using var enumerator = source.GetEnumerator();
            if (!enumerator.MoveNext())
                throw new InvalidOperationException("Cannot select a random element from an empty collection.");

            T result = enumerator.Current;
            int count = 1;

            while (enumerator.MoveNext())
            {
                count++;
                if (_random.Next(count) == 0)
                    result = enumerator.Current;
            }

            return result;
        }
    }
}
