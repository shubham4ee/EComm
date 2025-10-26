using UserServer.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserServer.DAL.Configurations
{
    internal class UserProjectsConfiguration : IEntityTypeConfiguration<UserProjects>
    {
        public void Configure(EntityTypeBuilder<UserProjects> builder)
        {
            builder.HasKey(up => new { up.UserId, up.ProjectId });

            builder.HasOne(upm => upm.User)
                .WithMany(u => u.UserProjects)
                .HasForeignKey(u => u.UserId);


            builder.HasOne(upm => upm.Project)
                .WithMany(p => p.UserProjects)
                .HasForeignKey(p => p.ProjectId);
       

        }
    }
}
