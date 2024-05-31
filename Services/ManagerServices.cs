using AMVTravels.Abstractions;
using AMVTravels.Abstractions.Services;
using AMVTravels.Common;
using AMVTravels.Contexts;
using AMVTravels.Models.Dtos;
using AMVTravels.Models.Entities;
using AMVTravels.Models.ViewModels;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace AMVTravels.Services
{
    public class ManagerServices : IManagerServices
    {
        private readonly AppDbContext _context;
        private readonly IValidator<Tour> _tourValidator;
        private readonly IValidator<Reservation> _reservationValidator;

        public ManagerServices( AppDbContext context,
                                IValidator<Tour>tourValidator,
                                IValidator<Reservation>reservationValidator)
        {
            _context = context;
            _tourValidator = tourValidator;
            _reservationValidator = reservationValidator;
        }

        public async Task<IOperationResult> CreateReservation(ReservationViewModel reserv)
        {
            try
            {
                Reservation newReserv = new()
                {
                    ClientName = reserv.Reservations[0].ClientName,
                    ReservationDate = reserv.Reservations[0].ReservationDate,
                    TourId = reserv.Reservations[0].TourId,
                };

                var newReservValidation = await _reservationValidator.ValidateAsync(newReserv);
                if (!newReservValidation.IsValid)
                {
                    return OperationResult.Fail<ReservationDto>("No se pudieron validar los datos de la Reserva", newReservValidation.Errors.Select(x => x.ErrorMessage.ToString()));
                }

                _context.Reservations.Add(newReserv);
                await _context.SaveChangesAsync();
                
                var result = OperationResult.Success("La inserción de la Reserva se completó exitosamente");
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"La insercion de la Reserva se completó con errores: {ex.Message}");
            }
        }

        public async Task<IOperationResult> CreateTour(TourViewModel tour)
        {
            try
            {
                Tour newTour = new()
                {
                    Name = tour.Tours[0].Name,
                    Destination = tour.Tours[0].Destination,
                    StartDate = tour.Tours[0].StartDate,
                    EndDate = tour.Tours[0].EndDate,
                    Price = tour.Tours[0].Price
                };

                var newTourValidation = await _tourValidator.ValidateAsync(newTour);
                if (!newTourValidation.IsValid)
                {
                    return OperationResult.Fail<TourDto>("No se pudieron validar los datos del Tour", newTourValidation.Errors.Select(x => x.ErrorMessage.ToString()));
                }

                _context.Tours.Add(newTour);
                await _context.SaveChangesAsync();
                
                var result = OperationResult.Success("La inserción del Tour se completó exitosamente");
                return result;
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"La insercion del Tour se completó con errores: {ex.Message}");
            }
        }

        public async Task<IOperationResult> DeleteReservation(int id)
        {
            try
            {
                var delReserv = await _context.Reservations.Where(x => x.Id == id).ToListAsync();
                if (delReserv!= null && delReserv.Count == 1)
                {
                    _context.Reservations.Remove(delReserv[0]);
                    await _context.SaveChangesAsync();
                    return OperationResult.Success("La eliminación de la Reserva se completó exitosamente");

                }else
                {
                    throw new NotImplementedException();
                }
                 
            }
            catch(Exception ex)
            {
                return OperationResult.Fail("La eliminación de la reserva se completó con errores");
            }
        }

        public async Task<IOperationResult> DeleteTour(int id)
        {
            try
            {
                var delTour = await _context.Tours.Where(x => x.Id == id).ToListAsync();
                if (delTour != null && delTour.Count == 1)
                {
                    _context.Tours.Remove(delTour[0]);
                    await _context.SaveChangesAsync();
                    return OperationResult.Success("La eliminación del Tour se completó exitosamente");

                }
                else
                {
                    throw new NotImplementedException();
                }

            }
            catch (Exception ex)
            {
                return OperationResult.Fail("La eliminación del Tour se completó con errores");
            }
        }

        public async Task<IOperationResult<ReservationViewModel>> ShowReservations()
        {
            var data = await _context.Reservations.Select(x => new ReservationDto
            {
                Id = x.Id,
                ClientName=x.ClientName,
                ReservationDate = x.ReservationDate,
                TourName = x.Tour.Name
            }).ToListAsync();

            var a = new ReservationViewModel();
            a.Reservations = data;
            var result = OperationResult.Success(a);
            return result;
        }

        public async Task<IOperationResult<TourViewModel>> ShowTours()
        {
            var data = await _context.Tours.Select(x => new TourDto
            {
               Id=x.Id,
               Name=x.Name,
               Destination=x.Destination,
               StartDate =x.StartDate,
               EndDate =x.EndDate,
               Price=x.Price
            }).ToListAsync();

            var a = new TourViewModel();
            a.Tours = data;
            var result = OperationResult.Success(a);
            return result;
        }
    }
}
