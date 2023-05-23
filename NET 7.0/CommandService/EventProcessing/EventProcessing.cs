using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Entities;

namespace CommandService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly IMapper _mapper;

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            _scopeFactory = scopeFactory;
            _mapper = mapper;
        }
        public async Task ProcessEventAsync(string message)
        {
            var eventType = DetermineEvent(message);

            switch (eventType)
            {
                case EventType.PlatformPublished:
                    await AddPlatformAsync(message);
                    break;
                default:

                    break;
            }

        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determinig Event");

            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

            switch (eventType!.Event)
            {
                case "Platform_Published":
                    Console.WriteLine("--> Platform Published Event Detected");
                    return EventType.PlatformPublished;
                default:
                    Console.WriteLine("--> Could not Determined the event type");
                    return EventType.Undetermined;
            }
        }

        private async Task AddPlatformAsync(string platformPublishedMessage)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ICommandRepo>();

                Console.WriteLine($"Message: {platformPublishedMessage}");

                var platformPublishedDto = JsonSerializer.Deserialize<PlatformPublishedDto>(platformPublishedMessage);

                try
                {
                    var plat = _mapper.Map<Platform>(platformPublishedDto);
                    if (!await repo.ExternalPlatformExistAsync(plat.ExternalId))
                    {
                        await repo.CreatePlatformAsync(plat);
                        await repo.SaveChangesAsync();

                        Console.WriteLine("--> Platform Added!");
                    }
                    else
                    {
                        Console.WriteLine("--> Platform already exists");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"--> Could not add platform to DB {e.Message}");
                }
            }
        }

        enum EventType
        {
            PlatformPublished,
            Undetermined
        }
    }
}