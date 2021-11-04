using System;
using System.Collections.Generic;
using System.Text;
using DBEntity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DBEntity.EntityConfigs
{
    public class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("t_address");
            builder.HasKey(x => x.id);
        }
    }
}
