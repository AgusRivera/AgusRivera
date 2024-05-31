using AMVTravels.Abstractions;
using AMVTravels.Contexts;
using AMVTravels.Models.Dtos;
using AMVTravels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace AMVTravels.Models.Entities
{
    public class Reservation : EntityBase, IDisplayable<Reservation>
    {
        public string ClientName { get; set; }
        public DateTime ReservationDate {  get; set; }
        public int TourId { get; set; }
        public Tour Tour { get; set; }

        public async Task<Reservation> ShowData(AppDbContext context)
        {
            return await context.Reservations.FindAsync(Id);
        }
    }
}
