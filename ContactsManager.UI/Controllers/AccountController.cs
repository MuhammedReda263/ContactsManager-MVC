using ContactsManager.Core.Domain.Entities;
using ContactsManager.Core.DTO;
using ContactsManager.Core.Enums;
using CRUDExample.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ContactsManager.UI.Controllers
{
	[Route("[controller]/[action]")]
	//[AllowAnonymous]
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _userManager;
	    private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<ApplicationRole> _roleManager;

		public AccountController (UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<ApplicationRole> roleManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_roleManager = roleManager;
		}

		[HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
        [Authorize("NotAuthorized")]
		[ValidateAntiForgeryToken]
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

				if(registerDTO.UserType == UserTypeOptions.Admin)
				{
					if (await _roleManager.FindByNameAsync(UserTypeOptions.Admin.ToString())is null)
					{
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.Admin.ToString() };
						await _roleManager.CreateAsync(applicationRole);
					}
					await _userManager.AddToRoleAsync(user,UserTypeOptions.Admin.ToString());
				}
				else
				{
                    if (await _roleManager.FindByNameAsync(UserTypeOptions.User.ToString()) is null)
                    {
                        ApplicationRole applicationRole = new ApplicationRole() { Name = UserTypeOptions.User.ToString() };
                        await _roleManager.CreateAsync(applicationRole);
                    }
                    await _userManager.AddToRoleAsync(user, UserTypeOptions.User.ToString());

                }

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

		[HttpGet]
        [Authorize("NotAuthorized")]
        public IActionResult Login()
		{
			return View();
		}
		
		[HttpPost]
        [Authorize("NotAuthorized")]
        public async Task<IActionResult> Login(LoginDTO loginDTO, string ReturnUrl)
		{
			if (!ModelState.IsValid)
			{
                ViewBag.Errors = ModelState.Values.SelectMany(x => x.Errors).Select(temp => temp.ErrorMessage);
                return View(loginDTO);
            }
			// Check email and password are exsited or not and create cookie by default
			var result = await _signInManager.PasswordSignInAsync(loginDTO.Email, loginDTO.Password, isPersistent: false, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
				{
					return LocalRedirect(ReturnUrl);
				}
				return RedirectToAction(nameof(PersonsController.Index), "Persons");
			}
			ModelState.AddModelError("", "Invalid Email or password");
			return View(loginDTO);

		}
		[HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(PersonsController.Index), "Persons");
        }

		[AllowAnonymous]
		public async Task<IActionResult> IsEmailAlreadyRegistered (string email)
		{
			var user = await _userManager.FindByEmailAsync(email);
			if (user == null)
			{
				return Json(true);
			}
			else
			{
				return Json(false);
			}
		}
	}
}
