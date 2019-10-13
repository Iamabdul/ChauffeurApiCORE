using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/bookings")]
	[ApiController]
	public class BookingsController : ControllerBase
    {
        IChaufferDbContext context;
        ICreateBookingCommand createBookingCommand;
        IEditBookingCommand editBookingCommand;
        ICreateStopCommand createStopCommand;

        public BookingsController
            (
            IChaufferDbContext context,
            ICreateBookingCommand createBookingCommand,
            IEditBookingCommand editBookingCommand,
            ICreateStopCommand createStopCommand
            )
        {
            this.context = context;
            this.createBookingCommand = createBookingCommand;
            this.editBookingCommand = editBookingCommand;
            this.createStopCommand = createStopCommand;
        }

        public IEnumerable<Booking> GetAllBookings()
        {
            return context.Bookings.OrderByDescending(b => b.StartDate).ToList();
        }

        public IEnumerable<Booking> GetActiveInactiveBookings([FromQuery] bool isActive)
        {
            return context
					.Bookings
					.Where(d => isActive == true ? d.CancelledDate == null : d.CancelledDate != null)
					.OrderByDescending(dr => dr.StartDate)
					.ToList();
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> CreateBooking(BookingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await createBookingCommand.Execute(model);

            return Ok();
        }


        [Route("Edit")]
        [HttpPut]
        public async Task<ActionResult> EditBooking(BookingBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await editBookingCommand.Execute(model);

            return Ok();
        }

        [Route("Stops")]
        [HttpGet]
        public IQueryable<Stop> GetAllStops()
        {
            return context.Stops.OrderByDescending(s => s.Date);
        }

        [Route("Stops/Create")]
        [HttpPost]
        public async Task<ActionResult> CreateStop(StopBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await createStopCommand.Execute(model);

            return Ok();
        }
    }
}