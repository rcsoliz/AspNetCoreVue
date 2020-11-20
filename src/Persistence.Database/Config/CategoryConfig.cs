using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence.Database.Config
{
    public class CategoryConfig
    {
        public CategoryConfig(EntityTypeBuilder<Category> entityTypeBuilder)
        {
            entityTypeBuilder.HasKey(x => x.CategoryId);
            entityTypeBuilder.Property(x => x.Name).IsRequired().HasMaxLength(80);
        }
    }
}
