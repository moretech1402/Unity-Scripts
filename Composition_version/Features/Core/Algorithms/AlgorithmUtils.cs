using System.Collections.Generic;

namespace Core.Algorithms
{
    public static class AlgorithmUtils
    {
        // Helper method to generate all subsets of a list.
        public static IEnumerable<List<T>> GetSubsets<T>(List<T> list)
        {
            for (int i = 0; i < (1 << list.Count); i++)
            {
                var subset = new List<T>();
                for (int j = 0; j < list.Count; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        subset.Add(list[j]);
                    }
                }
                yield return subset;
            }
        }
    }

}
