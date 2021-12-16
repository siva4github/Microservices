using AutoMapper;
using CommandService.Models;
using CommandsService.Dtos;

namespace CommandsService.Profiles
{
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            // Source --> Target
            CreateMap<Platform, PlatformReadDto>();
            CreateMap<CommandCreateDto, Command>();
            CreateMap<Command, CommandReadDto>();

            // After RabbitMQ
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest=> dest.ExternalId, opt=> opt.MapFrom(src => src.Id));
        }
    }
}