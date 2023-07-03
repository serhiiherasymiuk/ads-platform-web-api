using Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Configurations
{
    public class AdvertismentConfigurations : IEntityTypeConfiguration<Advertisment>
    {
        public void Configure(EntityTypeBuilder<Advertisment> builder)
        {
            builder
                .HasMany(a => a.AdvertismentImages)
                .WithOne(i => i.Advertisment)
                .HasForeignKey(i => i.AdvertismentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
