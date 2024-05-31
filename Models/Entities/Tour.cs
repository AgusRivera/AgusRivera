using AMVTravels.Abstractions;
using AMVTravels.Contexts;
using AMVTravels.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace AMVTravels.Models.Entities
{
    public class Tour : EntityBase, IDisplayable<Tour>
    {
        public string Name { get; set; }
        public string Destination { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public decimal Price {  get; set; }
        public ICollection<Reservation> Reservations { get; set; }

        public async Task<Tour> ShowData(AppDbContext context)
        {
            return await context.Tours.FindAsync(Id);
        }
    }
}
