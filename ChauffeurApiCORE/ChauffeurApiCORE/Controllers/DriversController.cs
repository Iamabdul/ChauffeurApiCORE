using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
    [Route("api/Drivers")]
    public class DriversController : ControllerBase
    {
        IChaufferDbContext context;
        ICreateDriverCommand createDriverCommand;

        public DriversController(ICreateDriverCommand createDriverCommand, IChaufferDbContext context)
        {
            this.context = context;
            this.createDriverCommand = createDriverCommand;
        }

        public IQueryable<Driver> GetAllDrivers()
        {
            return context.Dirvers.OrderByDescending(dr => dr.LastBookingDate);
        }

        [Route("ActiveInactive")]
        public IQueryable<Driver> GetActiveInactiveDrivers([FromRoute] bool isActive)
        {
            return context.Dirvers.Where(d => d.IsActive == isActive).OrderByDescending(dr => dr.LastBookingDate);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Register(Driver model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await createDriverCommand.Execute(model);

            return Ok();
        }
    }
}
