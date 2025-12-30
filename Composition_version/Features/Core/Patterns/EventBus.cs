using System;
using System.Collections.Generic;

namespace MC.Core.Patterns
{
    public interface IEventBus
    {
        void Subscribe<TEvent>(Action<TEvent> handler);
        void Unsubscribe<TEvent>(Action<TEvent> handler);
        void Publish<TEvent>(TEvent evt);
    }

    public sealed class EventBus : IEventBus
    {
        private readonly Dictionary<Type, Delegate> _handlers = new();

        public void Subscribe<TEvent>(Action<TEvent> handler)
        {
            var type = typeof(TEvent);

            if (_handlers.TryGetValue(type, out var existing))
                _handlers[type] = Delegate.Combine(existing, handler);
            else
                _handlers[type] = handler;
        }

        public void Unsubscribe<TEvent>(Action<TEvent> handler)
        {
            var type = typeof(TEvent);

            if (!_handlers.TryGetValue(type, out var existing))
                return;

            var newDelegate = Delegate.Remove(existing, handler);

            if (newDelegate == null)
                _handlers.Remove(type);
            else
                _handlers[type] = newDelegate;
        }

        public void Publish<TEvent>(TEvent evt)
        {
            var type = typeof(TEvent);

            if (_handlers.TryGetValue(type, out var existing))
                ((Action<TEvent>)existing)?.Invoke(evt);
        }
    }

    /// <summary>
    /// Global access point to a shared EventBus instance.
    /// Intended for bootstrap and early lifecycle events.
    /// </summary>
    public static class GlobalEventBus
    {
        public static IEventBus Instance { get; private set; } = new EventBus();

        /// <summary>
        /// Replaces the global event bus instance.
        /// Intended for bootstrap or tests only.
        /// </summary>
        public static void Set(IEventBus eventBus)
        {
            Instance = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        public static void Subscribe<TEvent>(Action<TEvent> handler)
            => Instance.Subscribe(handler);

        public static void Unsubscribe<TEvent>(Action<TEvent> handler)
            => Instance.Unsubscribe(handler);

        public static void Publish<TEvent>(TEvent evt)
            => Instance.Publish(evt);

        /// <summary>
        /// Clears all listeners. Use with care (e.g. between play sessions).
        /// </summary>
        public static void Reset()
        {
            Instance = new EventBus();
        }
    }

}
