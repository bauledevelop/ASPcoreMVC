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
    public class CategoryProductConfig : IEntityTypeConfiguration<CategoryProduct>
    {
        public void Configure(EntityTypeBuilder<CategoryProduct> builder)
        {
            builder.ToTable("CategoryProducts");
            builder.HasKey(c => c.ID);
            builder.HasOne<Account>(c => c.Account).WithMany(a => a.CategoryProducts).HasForeignKey(c => c.CreatedBy).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
