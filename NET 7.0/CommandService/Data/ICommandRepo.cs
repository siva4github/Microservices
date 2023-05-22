using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Entities;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        // Platforms
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task CreatePlatformAsync(Platform platform);
        Task<bool> PlatformExist(int platformId);

        // Commands
        Task<IEnumerable<Command>> GetCommandsForPlatformAsync(int platformId);
        Task<Command?> GetCommandAsync(int platformId, int commandId);
        Task CreateCommandAsync(Command command);

        Task<bool> SaveChangesAsync();
    }
}