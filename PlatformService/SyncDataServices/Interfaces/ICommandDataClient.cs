using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Dtos;

namespace PlatformService.SyncDataServices.Interfaces
{
    public interface ICommandDataClient
    {
        Task SendPlatformToCommandAsync(PlatformReadDto platform);
    }
}