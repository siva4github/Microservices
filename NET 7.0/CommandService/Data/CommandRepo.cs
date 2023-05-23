using CommandService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task CreateCommandAsync(Command command)
        {
            if(command is null)
                throw new ArgumentNullException(nameof(command));

            await _context.Commands.AddAsync(command);
        }

        public async Task CreatePlatformAsync(Platform platform)
        {
            if(platform is null)
                throw new ArgumentNullException(nameof(platform));

            await _context.Platforms.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatformsAsync()
        {
            return await _context.Platforms.ToListAsync();
        }

        public async Task<Command?> GetCommandAsync(int platformId, int commandId)
        {
            return await _context.Commands.FirstOrDefaultAsync(x=> x.PlatformId == platformId && x.Id == commandId);
        }

        public async Task<IEnumerable<Command>> GetCommandsForPlatformAsync(int platformId)
        {
            return await _context.Commands.Where(x=> x.PlatformId == platformId).ToListAsync();
        }

        public async Task<bool> PlatformExist(int platformId)
        {
            return await _context.Platforms.AnyAsync(p => p.Id == platformId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() >= 0;
        }

        public async Task<bool> ExternalPlatformExistAsync(int externalPlatformId)
        {
            return await _context.Platforms.AnyAsync(p=> p.ExternalId == externalPlatformId);
        }
    }
}