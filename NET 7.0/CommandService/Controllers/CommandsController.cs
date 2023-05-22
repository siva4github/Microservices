using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using CommandService.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/platforms/{platformId}/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly ICommandRepo _commandRepo;
        private readonly IMapper _mapper;
        public CommandsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _commandRepo = commandRepo ?? throw new ArgumentNullException(nameof(commandRepo));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommandReadDto>>> GetCommandsForPlatform(int platformId)
        {
            Console.WriteLine($"--> Hit GetCommandsForPlatform : {platformId} ");

            if (!await _commandRepo.PlatformExist(platformId))
            {
                return NotFound($"Platform not exist with id: {platformId}");
            }

            var commands = await _commandRepo.GetCommandsForPlatformAsync(platformId);

            return Ok(_mapper.Map<IEnumerable<CommandReadDto>>(commands));

        }

        [HttpGet("{commandId}", Name = "GetCommandForPlatform")]
        public async Task<ActionResult<CommandReadDto>> GetCommandForPlatform(int platformId, int commandId)
        {
            Console.WriteLine($"--> Hit GetCommandForPlatform : {platformId} - {commandId} ");

            if (!await _commandRepo.PlatformExist(platformId))
            {
                return NotFound($"Platform not exist with id: {platformId}");
            }

            var command = await _commandRepo.GetCommandAsync(platformId, commandId);

            return Ok(_mapper.Map<CommandReadDto>(command));

        }

        [HttpPost]
        public async Task<ActionResult<CommandReadDto>> CreateCommandForPlatform(int platformId, CommandCreateDto commandCreateDto)
        {
            Console.WriteLine($"--> Hit CreateCommandForPlatform : {platformId} ");

            if (!await _commandRepo.PlatformExist(platformId))
            {
                return NotFound($"Platform not exist with id: {platformId}");
            }

            var command = _mapper.Map<Command>(commandCreateDto);
            command.PlatformId = platformId;

            await _commandRepo.CreateCommandAsync(command);

            await _commandRepo.SaveChangesAsync();

            return CreatedAtRoute(nameof(GetCommandForPlatform), new { platformId = platformId, commandId = command.Id }, commandCreateDto);

        }

    }
}