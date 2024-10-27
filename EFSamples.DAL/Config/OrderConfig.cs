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
    public class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            //Default Value
            builder.Property(o => o.CreateDate)
                .HasDefaultValue(DateTime.Now);

            //Query Filter
            builder.HasQueryFilter(o=>!o.IsRemoved);

            //Order-Datetime.now-Id
            builder.Property(o => o.Code)
                .HasComputedColumnSql("'Order-'+Cast(getdate() As varchar)+ '-' + Cast(Id as varchar) ");

            //Shadow Property
            builder.Property<DateTime>("UpdateDate")
                .HasDefaultValueSql("getdate()");


        }
    }
}
