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
    public class FileConfig : IEntityTypeConfiguration<File>
    {
        public void Configure(EntityTypeBuilder<File> builder)
        {
            builder.ToTable("Files");
            builder.HasKey(a => a.ID);
            builder.HasOne<Account>(f => f.Account).WithMany(a => a.Files).HasForeignKey(f => f.CreatedBy).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Product>(f => f.Product).WithMany(f => f.Files).HasForeignKey(f => f.IDProduct).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
