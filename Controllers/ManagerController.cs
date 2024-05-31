using AMVTravels.Abstractions;
using AMVTravels.Abstractions.Services;
using AMVTravels.Models.Dtos;
using AMVTravels.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AMVTravels.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IManagerServices _managerServices;
        private readonly ILogger<ManagerController> _logger;

        public ManagerController(IManagerServices managerServices, ILogger<ManagerController> logger)
        {
            _managerServices = managerServices;
            _logger = logger;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var data = await _managerServices.ShowReservations();
            return View(data);
        }

        [HttpGet]
        public async Task<ActionResult<IOperationResult<TourViewModel>>> GetTours()
        {
            var result = await _managerServices.ShowTours();
            return Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<IOperationResult<ReservationViewModel>>> GetReservations()
        {
            var result = await _managerServices.ShowReservations();
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IOperationResult>> CreateTours([FromBody] TourViewModel tour)
        {
            var result = await _managerServices.CreateTour(tour);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<IOperationResult>> CreateReservation([FromBody] ReservationViewModel reservation)
        {
            var result = await _managerServices.CreateReservation(reservation);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<IOperationResult>> DeleteTour(int id)
        {
            var result = await _managerServices.DeleteTour(id);
            return Ok(result);
        }

        [HttpDelete]
        public async Task<ActionResult<IOperationResult>> DeleteReservation(int id)
        {
            var result = await _managerServices.DeleteReservation(id);
            return Ok(result);
        }




    }
}
