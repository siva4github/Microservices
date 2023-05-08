using AutoMapper;
using PlatformService.Dtos;
using PlatformService.Models;

namespace PlatformService.Profiles
{
    public class PlatformsProfiles : Profile
    {
        public PlatformsProfiles()
        {
            // Source --> Target

            CreateMap<PlatformCreateDto, Platform>();
            CreateMap<Platform,PlatformReadDto>();

            // After Rabbit
            CreateMap<PlatformReadDto, PlatformPublishedDto>();

            // After Grpc
            CreateMap<Platform, GrpcPlatformModel>()
                .ForMember(dest => dest.PlatformId, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.Publisher));
        }
    }
}