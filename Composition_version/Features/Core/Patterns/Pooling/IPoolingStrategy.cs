using System.Collections.Generic;

namespace Core.Patterns.Pooling
{
    /// <summary> Interface to define the strategy of how objects are handled within the pool. </summary>
    public interface IPoolingStrategy<T>
    {
        /// <summary> Add an object to the pool. </summary>
        void Add(T obj);

        /// <summary> Gets an object of pool. </summary>
        T Get();

        /// <summary> Get the current number of objects in the pool. </summary>
        int Count { get; }

        /// <summary> Clean all objects of the strategy. </summary>
        void Clear();

        /// <summary> Verifica si el pool contiene un objeto específico. </summary>
        bool Contains(T obj);

        /// <summary> It allows it to iterate on the objects in the pool. </summary>
        IEnumerable<T> AllObjects { get; }
    }
}