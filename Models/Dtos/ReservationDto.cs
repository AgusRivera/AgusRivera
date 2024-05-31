using AMVTravels.Models.Entities;

namespace AMVTravels.Models.Dtos
{
    public class ReservationDto : Reservation
    {
        public string TourName { get; set; }
    }
}
