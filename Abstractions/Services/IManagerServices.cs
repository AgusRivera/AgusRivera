using AMVTravels.Models.Dtos;
using AMVTravels.Models.Entities;
using AMVTravels.Models.ViewModels;

namespace AMVTravels.Abstractions.Services
{
    public interface IManagerServices
    {
        //!Tour
        Task<IOperationResult<TourViewModel>> ShowTours();
        Task<IOperationResult> CreateTour(TourViewModel tour);
        Task<IOperationResult> DeleteTour(int id);

        //!Reservation
        Task<IOperationResult<ReservationViewModel>> ShowReservations();
        Task<IOperationResult> CreateReservation(ReservationViewModel reservation);
        Task<IOperationResult> DeleteReservation(int id);
    }
}
