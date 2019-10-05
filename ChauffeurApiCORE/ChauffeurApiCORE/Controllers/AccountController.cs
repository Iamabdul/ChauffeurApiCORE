using System.Threading.Tasks;
using ChauffeurApiCORE.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ChauffeurApiCORE.Controllers
{
	[Route("api/account")]
	[ApiController]
	public class AccountController : ControllerBase
	{
		readonly UserManager<ApplicationUser> userManager;

		public AccountController(UserManager<ApplicationUser> userManager)
		{
			this.userManager = userManager;
		}

		// POST api/Account/Register
		[AllowAnonymous]
		[Route("Register")]
		public async Task<ActionResult> Register(RegisterBindingModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}
			var user = new ApplicationUser { UserName = model.Email, Email = model.Email, PhoneNumber = model.PhoneNumber };

			var result = await userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				return GetErrorResult(result);
			}

			return Ok();
		}

		ActionResult GetErrorResult(IdentityResult result)
		{
			if (result == null)
			{
				return BadRequest();
			}

			if (!result.Succeeded)
			{
				if (result.Errors != null)
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(error.Code, error.Description);
					}
				}

				if (ModelState.IsValid)
				{
					// No ModelState errors are available to send, so just return an empty BadRequest.
					return BadRequest();
				}

				return BadRequest(ModelState);
			}

			return null;
		}
	}
}
