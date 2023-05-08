using AutoMapper;
using CommandService.Models;
using CommandsService.Dtos;
using PlatformService;

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

            // After Grpc
            CreateMap<GrpcPlatformModel, Platform>()
                .ForMember(dest=> dest.ExternalId, opt => opt.MapFrom(src => src.PlatformId))
                .ForMember(dest=> dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest=> dest.Commands, opt => opt.Ignore());
        }
    }
}