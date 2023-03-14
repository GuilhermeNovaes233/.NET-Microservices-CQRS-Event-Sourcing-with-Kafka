using CQRS.Core.Events;

namespace CQRS.Core.Infrastructure
{
	public interface IEventStore
	{
		Task SaveEventsAsync(Guid aggrageteId, IEnumerable<EventModel> events, int expectedVersion);
		Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId);
	}
}