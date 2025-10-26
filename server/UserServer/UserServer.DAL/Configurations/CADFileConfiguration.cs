using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UserServer.DAL.Models;

namespace UserServer.DAL.Configurations
{
    public class CADFileConfiguration : IEntityTypeConfiguration<CADFile>
    {
        public void Configure(EntityTypeBuilder<CADFile> builder)
        {
            builder.HasKey(f => f.Id);
            builder.HasIndex(f => new { f.FileName, f.ProjectId })
                .IsUnique();
            builder.Property(f => f.Id).HasDefaultValueSql("uuid_generate_v4()").IsRequired();

            builder.Property(f => f.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.FilePath)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(f => f.ConversionStatus).IsRequired().HasMaxLength(50).HasDefaultValue("Not Initiated");

            builder.HasOne(f => f.Project)
                .WithMany(p => p.CADFiles)
                .HasForeignKey(f => f.ProjectId);

            builder.HasOne(f => f.User)
                .WithMany()
                .HasForeignKey(f => f.UploadedBy);

        }
    }
}
