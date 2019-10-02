using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Models;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/customers")]
	[ApiController]
	public class CustomersController : ControllerBase
    {
        IChaufferDbContext context;
        ICreateCustomerCommand createCustomerCommand;

        public CustomersController(ICreateCustomerCommand createCustomerCommand, IChaufferDbContext context)
        {
            this.context = context;
            this.createCustomerCommand = createCustomerCommand;
        }

        public IQueryable<Customer> GetAllCustomers()
        {
            return context.Customers.OrderByDescending(b => b.FirstName);
        }

		[Route("")]
        public IQueryable<Customer> GetCustomers([FromRoute] string searchTerm)
        {
            return context.Customers
                .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm))
                .OrderByDescending(c => c.FirstName);
        }

        [Route("Create")]
        [HttpPost]
        public async Task<ActionResult> Register(CustomerBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await createCustomerCommand.Execute(model);

            return Ok();
        }
    }
}
