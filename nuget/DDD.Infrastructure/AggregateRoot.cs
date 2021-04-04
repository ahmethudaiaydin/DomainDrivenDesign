using System.Collections.Generic;

namespace Shared.Infrastructure
{
    public abstract class AggregateRoot<T> : IAggregateRoot<T>
    {
        //May not be static. If we move Create functionality to constructors
        private readonly static List<IEvent> _events;

        public T Id { get; protected set; }

        public IReadOnlyCollection<IEvent> Events => _events;

        //May not be static. If we move Create functionality to constructors
        protected static void RegisterEvent(IEvent @event)
        {
            _events.Add(@event);
        }

    }
}
