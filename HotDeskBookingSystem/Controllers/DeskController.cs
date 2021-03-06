using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using HotDeskBookingSystem.Entities;
using HotDeskBookingSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotDeskBookingSystem.Controllers
{
    [Route("api/desk")]
    [ApiController]
    public class DeskController : Controller
    {
        private readonly IDeskService _service;

        public DeskController(IDeskService service)
        {
            _service = service;
        }

        [HttpPost("location")]
        [Authorize(Roles = "Admin")]
        public ActionResult AddLocation([FromBody] Location location)
        {
            var isAdded = _service.AddLocation(location);

            if(!isAdded)
            {
                return NotFound();
            }

            return Ok();
        }

        [HttpDelete("location/{locationId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveLocation([FromRoute] int locationId)
        {
            var isRemoved = _service.RemoveLocation(locationId);

            if (!isRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult AddDesk([FromBody] Desk desk)
        {
            var isRemoved = _service.AddDesk(desk);

            if (!isRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{deskId}")]
        [Authorize(Roles = "Admin")]
        public ActionResult RemoveDesk([FromRoute] int deskId)
        {
            var isRemoved = _service.RemoveDesk(deskId);

            if (!isRemoved)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<Desk>> GetAll(
            [FromQuery] DateTime start,
            [FromQuery] DateTime finish,
            [FromQuery] string? orderBy = "id",
            [FromQuery(Name = "available")] bool isAvailable = false)
        {
            var desks = _service.GetAll(orderBy, isAvailable, start, finish);
            
            return Ok(desks);
        }

        [HttpPost("reserve/{deskId}")]
        [Authorize]
        public ActionResult BookDesk(
            [FromRoute] int deskId,
            [FromQuery] DateTime start,
            [FromQuery] DateTime finish)
        {
            var employeeId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isCreated = _service.CreateReservation(deskId, Int32.Parse(employeeId), start, finish);

            if (isCreated)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPost("change/{reservationId}")]
        [Authorize]
        public ActionResult ChangeDesk(
            [FromRoute] int reservationId,
            [FromQuery] int deskId)
        {
            var employeeId = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var isChanged = _service.ChangeDesk(reservationId, deskId, Int32.Parse(employeeId));

            if (isChanged)
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}

