using AMVTravels.Models.Entities;

namespace AMVTravels.Models.Dtos
{
    public class TourDto : Tour
    {
        public TourDto()
        {
            Reservations = null; 
        }
    }
}
