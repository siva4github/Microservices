using AutoMapper;
using CommandService.Dtos;
using CommandService.Entities;

namespace CommandService.Profiles
{
    public class CommandProfiles : Profile
    {
        public CommandProfiles()
        {
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();
            CreateMap<Platform, PlatformReadDto>();
        }
    }
}