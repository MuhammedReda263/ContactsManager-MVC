using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.DTO;
using CRUDExample.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
	[Route("[controller]/[action]")]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
	    private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> Register(RegisterDTO registerDTO)
		{
			//Check Validation Errors 
			if (ModelState.IsValid == false)
			{
				ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(temp=>temp.ErrorMessage); 
				return View(registerDTO);
			}
			//TO DO: Store user registration details into Identity database

			ApplicationUser user = new ApplicationUser() {
				Email = registerDTO.Email,
				PhoneNumber = registerDTO.Phone,
				UserName = registerDTO.PersonName,
				PersonName = registerDTO.PersonName
			};

			IdentityResult result = await _userManager.CreateAsync(user,registerDTO.Password);
			if (result.Succeeded)
			{
				await _signInManager.SignInAsync(user, isPersistent: false); // Creat Cookies and send it to browser
				return RedirectToAction(nameof(PersonsController.Index), "Persons");
			}
			else
			{
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError("Register", error.Description);
				}
			}
			return View(registerDTO);
		}
	}
}
