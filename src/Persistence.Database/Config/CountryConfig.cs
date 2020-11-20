using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Database.Config
{
    public class CountryConfig
    {
        public CountryConfig(EntityTypeBuilder<Country> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.CountryId);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(60);
        }
    }
}
