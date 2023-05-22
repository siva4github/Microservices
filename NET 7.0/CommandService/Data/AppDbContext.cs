using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommandService.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Platform> Platforms { get; set; } = default!;
        public DbSet<Command> Commands { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Platform>().HasMany(x => x.Commands).WithOne(x => x.Platform).HasForeignKey(x => x.PlatformId);
            modelBuilder.Entity<Command>().HasOne(x => x.Platform).WithMany(x => x.Commands).HasForeignKey(x => x.PlatformId);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
    }
}