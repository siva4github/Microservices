using System.Collections.Generic;
using System.Linq;
using CommandService.Models;

namespace CommandsService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context;
        }
        public void CreateCommand(int platformId, Command command)
        {
            if(command == null)
            {
                throw new System.ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            _context.Commands.Add(command);

        }

        public void CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new System.ArgumentNullException(nameof(platform));
            }
            _context.Platforms.Add(platform);
        }

        public IEnumerable<Platform> GetAllPlatforms()
        {
            return _context.Platforms.ToList();
        }

        public Command GetCommand(int platformId, int commandId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId && c.Id == commandId).FirstOrDefault();
        }

        public IEnumerable<Command> GetCommandsForPlatform(int platformId)
        {
            return _context.Commands.Where(c => c.PlatformId == platformId).OrderBy(c => c.Platform.Name);
        }

        public bool PlatformExist(int platformId)
        {
            return _context.Platforms.Any(p => p.Id == platformId);
        }

        public bool SaveChages()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}