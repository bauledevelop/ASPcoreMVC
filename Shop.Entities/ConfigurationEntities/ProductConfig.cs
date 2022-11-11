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
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(p => p.ID);
            builder.HasOne<Account>(p => p.Account).WithMany(a => a.Products).HasForeignKey(p => p.CreatedBy).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<CategoryProduct>(p => p.CategoryProduct).WithMany(c => c.Products).HasForeignKey(p => p.IDCategoryProduct).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
