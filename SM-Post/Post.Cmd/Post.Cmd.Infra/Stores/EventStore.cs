﻿using CQRS.Core.Domain;
using CQRS.Core.Events;
using CQRS.Core.Exceptions;
using CQRS.Core.Infrastructure;
using Post.Cmd.Domain.Aggregates;
using System.Data;

namespace Post.Cmd.Infra.Stores
{
	public class EventStore : IEventStore
	{
		private readonly IEventStoreRepository _eventStoreRepository;

		public EventStore(IEventStoreRepository eventStoreRepository)
		{
			_eventStoreRepository = eventStoreRepository;
		}

		public async Task<List<BaseEvent>> GetEventsAsync(Guid aggregateId)
		{
			var eventStrem = await _eventStoreRepository.FindByAggregateId(aggregateId);

			if (eventStrem == null || !eventStrem.Any())
				throw new AggregateNotFoundException("Incorrect post ID provided!");

			return eventStrem
				.OrderBy(x => x.Version)
				.Select(x => x.EventData)
				.ToList();
		}


		public async Task SaveEventsAsync(Guid aggregateId, IEnumerable<BaseEvent> events, int expectedVersion)
		{
			var eventStream = await _eventStoreRepository.FindByAggregateId(aggregateId);

			if (expectedVersion != -1 && eventStream[^1].Version != expectedVersion)
				throw new ConcurrencyException();

			var version = expectedVersion;

			foreach (var @event in events)
			{
				version++;
				@event.Version = version;
				var eventType = @event.GetType().Name;
				var eventModel = new EventModel
				{
					TimeStamp = DateTime.Now,
					AggregateIdentifier = aggregateId,
					AggregateType = nameof(PostAggregate),
					Version = version,
					EventType = eventType,
					EventData = @event
				};

				await _eventStoreRepository.SaveAsync(eventModel);
			}
		}
	}
}