using System.Collections.Generic;

namespace MC.Core.Maths.Aggregation
{
    public interface IValueAggregationStrategy
    {
        float Aggregate(IEnumerable<float> values);
    }
}
