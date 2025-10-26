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
    public class ModelMetadataConfiguration : IEntityTypeConfiguration<ModelMetadata>
    {
        public void Configure(EntityTypeBuilder<ModelMetadata> builder)
        {
            builder.HasKey(m => m.Id);

            builder.Property(m => m.Metadata)
                .IsRequired()
                .HasColumnType("jsonb");

            builder.HasOne(m => m.CADFile)
                .WithOne(c => c.Metadata)
                .HasForeignKey<ModelMetadata>(m => m.CADFileId);
        }
    }
}
