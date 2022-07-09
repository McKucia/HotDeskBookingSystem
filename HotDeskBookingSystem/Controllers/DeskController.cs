using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotDeskBookingSystem.Entities;
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
            [FromQuery] string orderBy = "id",
            [FromQuery(Name = "available")] bool isAvailable = false)
        {
            var desks = _service.GetAll(orderBy, isAvailable);

            return Ok(desks);
        }
    }
}

