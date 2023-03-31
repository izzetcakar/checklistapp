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
    internal class ListItemConfiguration : IEntityTypeConfiguration<ListItem>
    {
        public void Configure(EntityTypeBuilder<ListItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Risk).HasConversion<String>();
            builder.Property(x => x.Result).HasColumnType("decimal(4,2)");
            builder.HasOne(x => x.Checklist).WithMany(x => x.ListItems).HasForeignKey(x => x.CheckListId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Segment).WithMany(x => x.ListItems).HasForeignKey(x => x.SegmentId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Category).WithMany(x => x.ListItems).HasForeignKey(x => x.CategoryId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.ControlList).WithMany(x => x.ListItems).HasForeignKey(x => x.ControlListId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Consept).WithMany(x => x.ListItems).HasForeignKey(x => x.ConseptId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(x => x.Content).WithMany(x => x.ListItems).HasForeignKey(x => x.ContentId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
