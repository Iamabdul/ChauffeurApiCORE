using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Exceptions;
using ChauffeurApiCORE.Models;

namespace ChauffeurApiCORE.Commands
{
	public class EditDriverCommand : IEditDriverCommand
    {
        private IChaufferDbContext context;
        public EditDriverCommand(IChaufferDbContext context)
        {
            this.context = context;
        }

        public async Task<Driver> Execute(Driver model)
        {
            var driver = context.Dirvers.FirstOrDefault(d => d.DriverId == model.DriverId);
            if (driver == null) throw new DriverNotFoundException("Driver Not Found");


            driver.Address = model.Address;
            driver.FirstName = model.FirstName;
            driver.LastName = model.LastName;
            driver.PreferredName = model.PreferredName;
            driver.PostCode = model.PostCode;
            driver.PhoneNumber = model.PhoneNumber;
            driver.LicenceNumber = model.LicenceNumber;
            driver.CarDetails = model.CarDetails;
            driver.CarType = model.CarType;
            driver.IsActive = model.IsActive;


            await context.SaveChangesAsync();

            return driver;
        }
    }

    public interface IEditDriverCommand
    {
            Task<Driver> Execute(Driver model);
    }
}