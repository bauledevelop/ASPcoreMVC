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
    public class PaymentConfig : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");
            builder.HasKey(p => p.ID);
            builder.HasOne<Account>(p => p.Account).WithMany(a => a.Payments).HasForeignKey(p => p.IDAccount).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Order>(p => p.Order).WithMany(o => o.Payments).HasForeignKey(p => p.IDOrder).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
