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
    public class ControlListConfiguration : IEntityTypeConfiguration<ControlList>
    {
        public void Configure(EntityTypeBuilder<ControlList> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
