
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Nazar1988.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Mapping
{
    public class Product_CategoryMap : IEntityTypeConfiguration<Product_Category>
    {
        public void Configure(EntityTypeBuilder<Product_Category> builder)
        {
            builder.HasKey(t => new { t.ProductID, t.CategoryID });
            builder
                .HasOne(p => p.Product)
                .WithMany(c => c.book_Categories)
                .HasForeignKey(f => f.ProductID);

            builder
               .HasOne(p => p.Category)
               .WithMany(c => c.Product_Categories)
               .HasForeignKey(f => f.CategoryID);
        }
    }
}
