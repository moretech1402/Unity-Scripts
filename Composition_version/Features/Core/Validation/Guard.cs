using System;
using System.Collections.Generic;

namespace MC.Core.Validation
{
    public static class Guard
    {
        public static T NotNull<T>(T value, string name = null)
        {
            if (value == null)
                throw new ArgumentNullException(name ?? typeof(T).Name);

            return value;
        }

        public static string NotNullOrWhiteSpace(string value, string name = null)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Value cannot be null or whitespace", name);

            return value;
        }

        public static int Positive(int value, string name = null)
        {
            if (value < 0)
                throw new ArgumentOutOfRangeException(name, "Value must be positive");

            return value;
        }

        public static float Positive(float value, string name = null)
        {
            if (value < 0f)
                throw new ArgumentOutOfRangeException(name, "Value must be positive");

            return value;
        }

        public static T NotEmpty<T>(ICollection<T> collection, string name = null)
        {
            if (collection == null || collection.Count == 0)
                throw new ArgumentException("Collection cannot be empty", name);

            return (T)(object)collection;
        }

        public static IReadOnlyCollection<T> NotEmpty<T>(IReadOnlyCollection<T> collection, string name = null)
        {
            if (collection == null || collection.Count == 0)
                throw new ArgumentException("Collection cannot be empty", name);

            return collection;
        }
    }
}