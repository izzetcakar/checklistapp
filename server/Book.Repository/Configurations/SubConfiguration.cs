using Book.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Repository.Configurations
{
    public class SubConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x=> x.UserId).IsRequired();
            builder.Property(x=> x.OrganizationId).IsRequired();
            builder.HasOne(x => x.Organization).WithMany(x => x.Subs).HasForeignKey(x => x.OrganizationId);
            builder.HasOne(x => x.User).WithMany(x => x.Subs).HasForeignKey(x => x.UserId);
        }
    }
}
