using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Model;
namespace Persistence.Database.Config
{
    public class OrderDetailConfig
    {
        public OrderDetailConfig(EntityTypeBuilder<OrderDetail> entityTypeBuilder)
        {
            entityTypeBuilder.Property(x => x.Iva).IsRequired();
            entityTypeBuilder.Property(x => x.SubTotal).IsRequired();
            entityTypeBuilder.Property(x => x.Total).IsRequired();

        }
    }
}
