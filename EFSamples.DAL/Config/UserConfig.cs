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
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.CreateDate)
                .HasDefaultValue(DateTime.Now);

            builder.HasQueryFilter(u=>!u.IsRemoved);

            //Owns
            builder.OwnsOne(u=>u.Home);
            builder.OwnsOne(u => u.WorkPlace);

            //Conversion
            builder.Property(u => u.Username)
                .HasConversion(p=>Base64Encode(p),p=>Base64Decode(p));

        }

   

        public static string Base64Encode(string plainText)
        {
            var bytes=Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytes);
        }
        public static string Base64Decode(string plainText)
        {
            var plainTextByte = Convert.FromBase64String(plainText);
            return Encoding.UTF8.GetString(plainTextByte);
        }


    }
}
