using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserServer.DAL.Models;

namespace UserServer.DAL.Configurations
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.ProjectName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .HasColumnType("text");

            builder.Property(f => f.Status).IsRequired().HasMaxLength(50).HasDefaultValue("Created");

            builder.HasMany(p => p.Users)
                .WithMany(u => u.Projects);


        }
    }
}
