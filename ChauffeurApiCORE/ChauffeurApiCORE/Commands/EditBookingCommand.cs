using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Exceptions;
using ChauffeurApiCORE.Models;

namespace ChauffeurApiCORE.Commands
{
	public class EditBookingCommand : IEditBookingCommand
    {
		readonly IChaufferDbContext context;

        public EditBookingCommand(IChaufferDbContext context)
        {
			this.context = context;
        }

        public Task<Booking> Execute(BookingBindingModel model)
        {
            var currentBooking = context.Bookings.FirstOrDefault(b => b.BookingId == model.BookingId);

            if (currentBooking == null) throw new BookingNotFoundException("Booking not found");

            currentBooking.DriverId = model.DriverId;
            currentBooking.StartAddress = model.StartAddress;
            currentBooking.StartPostCode = model.StartPostCode;
            currentBooking.EndAddress = model.EndAddress;
            currentBooking.EndPostCode = model.EndPostCode;
            currentBooking.CompletedDate = model.CompletedDate ?? null;
            currentBooking.JobType = model.JobType;
            currentBooking.ExtraInformation = model.ExtraInformation;

            return Task.FromResult(currentBooking);
        }
    }

    public interface IEditBookingCommand
    {
        Task<Booking> Execute(BookingBindingModel model);
    }
}