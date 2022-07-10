using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotDeskBookingSystem.Entities;
using HotDeskBookingSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskBookingSystem.Controllers
{
    [Route("api/desk")]
    public class DeskController : Controller
    {
        private readonly IDeskService _service;

        public DeskController(IDeskService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Desk>> GetAll(
            [FromQuery] DateTime start,
            [FromQuery] DateTime finish,
            [FromQuery] string orderBy = "id",
            [FromQuery(Name = "available")] bool isAvailable = false)
        {
            var desks = _service.GetAll(orderBy, isAvailable, start, finish);
            
            return Ok(desks);
        }

        [HttpPost("reserve/{deskId}")]
        public ActionResult BookDesk(
            [FromRoute] int deskId,
            [FromQuery] int employeeId,
            [FromQuery] DateTime start,
            [FromQuery] DateTime finish)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isCreated = _service.CreateReservation(deskId, employeeId, start, finish);

            if (isCreated)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("change/{reservationId}")]
        public ActionResult ChangeDesk(
            [FromRoute] int reservationId,
            [FromQuery] int deskId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var isChanged = _service.ChangeDesk(reservationId, deskId);

            if (isChanged)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

