using Microsoft.EntityFrameworkCore;
using UserServer.DAL.Models;
using UserServer.DAL.Configurations;

namespace UserServer.DAL.DataContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CADFile> CADFiles { get; set; }

        public DbSet<UserProjects> UserProjects { get; set; }
        public DbSet<ModelMetadata> Metadata { get; set; }
        public DbSet<Permission> Permissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasPostgresExtension("uuid-ossp");
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ProjectConfiguration());
            modelBuilder.ApplyConfiguration(new CADFileConfiguration());
            modelBuilder.ApplyConfiguration(new ModelMetadataConfiguration());
            modelBuilder.ApplyConfiguration(new PermissionConfiguration());
            modelBuilder.ApplyConfiguration(new UserProjectsConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
