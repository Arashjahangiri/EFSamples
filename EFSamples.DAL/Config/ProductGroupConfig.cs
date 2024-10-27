using EFSamples.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFSamples.DAL.Config
{
    public class ProductGroupConfig : IEntityTypeConfiguration<ProductGroup>
    {
        public void Configure(EntityTypeBuilder<ProductGroup> builder)
        {
            builder.ToTable("ProductGroups");

            builder.Property(p => p.Title)
                .IsRequired()
                .HasColumnType("nvarchar")
                .HasMaxLength(150);

            builder.Property(p => p.Description)
                .IsRequired()
                .HasColumnType("nvarchar(max)");

            builder.HasQueryFilter(pg=>!pg.IsRemoved);

        }
    }
}
