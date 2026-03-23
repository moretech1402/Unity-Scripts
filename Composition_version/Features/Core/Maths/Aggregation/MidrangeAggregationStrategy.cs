using System.Collections.Generic;
using System.Linq;

namespace MC.Core.Maths.Aggregation
{
    public class MidrangeAggregationStrategy : IValueAggregationStrategy
    {
        public float Aggregate(IEnumerable<float> values)
            => (values.Min() + values.Max()) * 0.5f;
    }
}
