using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.AsyncDataServices;
using PlatformService.Dtos;
using PlatformService.Interfaces;
using PlatformService.Models;
using PlatformService.SyncDataServices.Http;

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

        public PlatformsController(IPlatformRepo repository, IMapper mapper, 
        ICommandDataClient commandDataClient, IMessageBusClient messageBusClient)
        {
            _mapper = mapper;
            _commandDataClient = commandDataClient;            
            _repository = repository;
            _messageBusClient = messageBusClient;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PlatformReadDto>> GetPlatforms()
        {
            var platforms = _repository.GetAllPlatforms();
            return Ok(_mapper.Map<IEnumerable<PlatformReadDto>>(platforms));
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<PlatformReadDto> GetPlatformById(int id)
        {
            var platform = _repository.GetPlatformById(id);

            if (platform == null) return NotFound();

            return Ok(_mapper.Map<PlatformReadDto>(platform));
        }

        [HttpPost]
        public async Task<ActionResult<PlatformReadDto>> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            if (platformCreateDto == null) BadRequest();

            var platformModel = _mapper.Map<Platform>(platformCreateDto);
            _repository.CreatePlatform(platformModel);
            _repository.SaveChages();

            var platformReadDto = _mapper.Map<PlatformReadDto>(platformModel);

            
            // add this logic for send sync messaging

              try{

                  await _commandDataClient.SendPlatformToCommand(platformReadDto);
              }  
              catch(Exception e)
              {
                  Console.WriteLine($"--> Could not send synchronously: {e.Message}");
              }

            // end


            // add this logic for send async messaging

            try
            {
                var platformPublishedDto = _mapper.Map<PlatformPublishedDto>(platformReadDto);
                platformPublishedDto.Event = "Platform_Published";
                Console.WriteLine(platformPublishedDto.Event);                
                _messageBusClient.PublishNewPlatform(platformPublishedDto);
            }
            catch(Exception e)
            {
                Console.WriteLine($"--> Could not send Asynchronously: {e.Message}");
            }


            // end


            return CreatedAtRoute(nameof(GetPlatformById), new { Id = platformReadDto.Id }, platformReadDto);
        }

    }
}