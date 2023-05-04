using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlatformService.Entities;

namespace PlatformService.Data.Interfaces
{
    public interface IPlatformRepo
    {
        Task CreatePlatformAsync(Platform platform);
        Task<Platform?> GetPlatformByIdAsync(int id);
        Task<IEnumerable<Platform>> GetAllPlatformsAsync();
        Task<bool> SaveChangesAsync();
    }
}