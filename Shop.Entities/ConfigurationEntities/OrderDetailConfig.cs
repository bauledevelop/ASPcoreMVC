using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shop.Entities.Enities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Entities.ConfigurationEntities
{
    public class OrderDetailConfig : IEntityTypeConfiguration<OrderDetail>
    {
        public void Configure(EntityTypeBuilder<OrderDetail> builder)
        {
            builder.ToTable("OrderDetails");
            builder.HasKey(o => o.ID);
            builder.HasOne<Order>(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(o => o.IDOrder).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Product>(o => o.Product).WithMany(p => p.OrderDetails).HasForeignKey(o => o.IDProduct).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
