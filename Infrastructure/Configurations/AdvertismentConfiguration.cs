using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class AdvertismentConfigurations : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(EntityTypeBuilder<Advertisement> builder)
        {
            builder
                .HasMany(a => a.AdvertisementImages)
                .WithOne(i => i.Advertisement)
                .HasForeignKey(i => i.AdvertisementId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
