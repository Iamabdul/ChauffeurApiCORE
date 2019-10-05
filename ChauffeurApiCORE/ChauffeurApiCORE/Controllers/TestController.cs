using System.Threading.Tasks;
using ChauffeurApiCORE.Commands;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/test")]
	[ApiController]
	public class TestController : ControllerBase
	{
		readonly ITestCommand testCommand;

		public TestController(ITestCommand testCommand)
		{
			this.testCommand = testCommand;
		}

		[HttpGet]
		public async Task<ActionResult> Hello()
		{
			var name = await testCommand.Execute();
			return Ok(name);
		}
	}
}