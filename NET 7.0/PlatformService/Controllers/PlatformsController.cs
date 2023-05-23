using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Entities;
using PlatformService.SyncDataServices.Interfaces;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        private readonly ICommandDataClient _commandDataClient;
        private readonly IMessageBusClient _messageBusClient;
        public PlatformsController(IPlatformRepo repository, IMapper mapper, ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commandDataClient = commandDataClient ?? throw new ArgumentNullException(nameof(commandDataClient));
            _messageBusClient = messageBusClient ?? throw new ArgumentNullException(nameof(messageBusClient));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);

            await _repository.CreatePlatformAsync(platform);
            await _repository.SaveChangesAsync();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platform);

            try
            {
                await _commandDataClient.SendPlatformToCommandAsync(platformReadDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Could not send synchronously : {e.Message} ");
            }

            // async messaging using RabbitMQ
            try
            {
                var platformPublishDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishDto.Event = "Platform_Published";
                _messageBusClient.PublishNewPlatform(platformPublishDto);
            }
            catch (Exception e)
            {
                Console.WriteLine($"--> Exception: {e.Message}");
            }


            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);

        }

        [HttpGet]
        public async Task<IActionResult> GetPlatforms()
        {
            var platforms = await _repository.GetAllPlatformsAsync();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public async Task<IActionResult> GetPlatformById(int id)
        {
            var platform = await _repository.GetPlatformByIdAsync(id);

            if (platform == null) return NotFound();

            return Ok(platform);
        }
    }
}