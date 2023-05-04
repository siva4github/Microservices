using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlatformService.Data.Interfaces;
using PlatformService.Dtos;
using PlatformService.Entities;

namespace PlatformService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepo _repository;
        private readonly IMapper _mapper;
        public PlatformController(IPlatformRepo repository, IMapper mapper)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlatform(PlatformCreateDto platformCreateDto)
        {
            var platform = _mapper.Map<Platform>(platformCreateDto);

            await _repository.CreatePlatformAsync(platform);
            if (await _repository.SaveChangesAsync())
                return Ok();

            return BadRequest();
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

            if(platform == null) return NotFound();

            return Ok(platform);
        }
    }
}