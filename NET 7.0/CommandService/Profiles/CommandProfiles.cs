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

            // After RabbitMQ
            CreateMap<PlatformPublishedDto, Platform>()
                .ForMember(dest=> dest.ExternalId, opt=> opt.MapFrom(src => src.Id))
                .ForMember(dest=> dest.Id, opt=> opt.Ignore());
        }
    }
}