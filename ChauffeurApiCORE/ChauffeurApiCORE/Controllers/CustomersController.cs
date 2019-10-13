using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using ChauffeurApiCORE.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/customers")]
	[ApiController]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	public class CustomersController : ControllerBase
    {
        IChaufferDbContext context;
        ICreateCustomerCommand createCustomerCommand;

        public CustomersController(ICreateCustomerCommand createCustomerCommand, IChaufferDbContext context)
        {
            this.context = context;
            this.createCustomerCommand = createCustomerCommand;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return context.Customers.OrderByDescending(b => b.FirstName).ToList();
        }

		[Route("search")]
        public IEnumerable<Customer> GetCustomers([FromQuery] string searchTerm)
        {
            return context.Customers
                .Where(c => c.FirstName.Contains(searchTerm) || c.LastName.Contains(searchTerm))
                .OrderByDescending(c => c.FirstName).ToList();
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