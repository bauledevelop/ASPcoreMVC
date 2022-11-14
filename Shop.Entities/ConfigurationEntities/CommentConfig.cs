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
    public class CommentConfig : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");
            builder.HasKey(a => a.ID);
            builder.HasOne<Account>(c => c.Account).WithMany(a => a.Comments).HasForeignKey(c => c.IDAccount).HasPrincipalKey(c => c.ID).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne<Product>(c => c.Product).WithMany(p => p.Comments).HasForeignKey(c => c.IDProduct).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
