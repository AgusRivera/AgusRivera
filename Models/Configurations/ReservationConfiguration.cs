using AMVTravels.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AMVTravels.Models.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.ToTable("Reservation");
            builder.HasKey(x => x.Id);
            builder.Property(prop => prop.ClientName).HasMaxLength(200).IsRequired();
            builder.Property(prop => prop.ReservationDate).IsRequired();
            builder.Property(prop => prop.TourId).IsRequired();
            //builder.HasOne(r => r.Tour).WithMany(r => r.Reservations).HasForeignKey(r => r.TourId);
        }
    }
}
