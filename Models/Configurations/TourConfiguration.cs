using AMVTravels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMVTravels.Models.Configurations
{
    public class TourConfiguration : IEntityTypeConfiguration<Tour>
    {
        public void Configure(EntityTypeBuilder<Tour> builder)
        {
            builder.ToTable("Tour");
            builder.HasKey(x => x.Id);
            builder.Property(prop => prop.Name).HasMaxLength(100).IsRequired();
            builder.Property(prop => prop.Destination).HasMaxLength(100).IsRequired();
            builder.Property(prop => prop.StartDate).IsRequired();
            builder.Property(prop => prop.Price).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
