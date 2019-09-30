using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("Api/Test")]
	public class TestController
	{
		[HttpGet]
		public ActionResult<string> Hello()
		{
			return "Hello";
		}
	}
}
