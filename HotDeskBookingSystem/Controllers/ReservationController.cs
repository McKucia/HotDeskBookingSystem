using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotDeskBookingSystem.Entities;
using HotDeskBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskBookingSystem.Controllers
{
    [Route("api/reservation")]
    public class ReservationController : Controller
    {
        private readonly IReservationService _service;

        public ReservationController(IReservationService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> GetAll()
        {
            return Ok();
        }
    }
}

