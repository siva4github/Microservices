using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CommandService.Data;
using CommandService.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CommandService.Controllers
{
    [Route("api/c/[controller]")]
    [ApiController]
    public class PlatformsController : ControllerBase
    {
        private ICommandRepo _commandRepo { get; }
        private readonly IMapper _mapper;
        public PlatformsController(ICommandRepo commandRepo, IMapper mapper)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _commandRepo = commandRepo ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> TestInboundConnection()
        {
            await Task.CompletedTask;

            Console.WriteLine("--> Inbound POST # Command Service");
            return Ok("Inbound test of from Platforms Controller");
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlatformReadDto>>> GetPlatforms()
        {
            Console.WriteLine("--> Getting Platforms from CommandsService");

            var platforms = await _commandRepo.GetAllPlatformsAsync();

            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }
    }
}