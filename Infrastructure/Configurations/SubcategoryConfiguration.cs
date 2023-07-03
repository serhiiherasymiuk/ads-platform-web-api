using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class SubcategoryConfigurations : IEntityTypeConfiguration<Subcategory>
    {
        public void Configure(EntityTypeBuilder<Subcategory> builder)
        {
            builder
                .HasMany(s => s.Advertisments)
                .WithOne(a => a.Subcategory)
                .HasForeignKey(a => a.SubcategoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
