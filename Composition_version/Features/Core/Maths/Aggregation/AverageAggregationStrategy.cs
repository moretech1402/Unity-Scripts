using System.Collections.Generic;
using System.Linq;

namespace MC.Core.Maths.Aggregation
{
    public class AverageAggregationStrategy : IValueAggregationStrategy
    {
        public float Aggregate(IEnumerable<float> values)
            => values.Average();
    }
}
