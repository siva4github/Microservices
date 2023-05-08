using System.Collections.Generic;
using CommandService.Models;

namespace CommandsService.Data
{
    public interface ICommandRepo
    {
        bool SaveChages();

        // Platforms
        IEnumerable<Platform> GetAllPlatforms();
        void CreatePlatform(Platform platform);
        bool PlatformExist(int platformId);
        // After RabbitMQ
        bool ExternalPlatformExist(int externalPlatformId);


        // Commands
        IEnumerable<Command> GetCommandsForPlatform(int platformId);
        Command GetCommand(int platformId, int commandId);
        void CreateCommand(int platformId, Command command);
    }
}