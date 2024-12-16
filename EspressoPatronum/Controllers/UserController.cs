using EspressoPatronum.Models.Entities;
using EspressoPatronum.Services;
using Microsoft.AspNetCore.Mvc;

namespace EspressoPatronum.Controllers
{
	public class UserController : Controller
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		// GET: /User/LogIn
		public IActionResult LogIn()
		{
			return View();
		}

		// POST: /User/LogIn
		[HttpPost]
		public IActionResult LogIn(User user)
		{
			bool isVerified = _userService.AuthenticateUser(user.Email, user.Password);

			if (isVerified)
			{
				Console.WriteLine("Yay! You exist.\n");

				// Handle user roles and redirection
				if (user.Role == "Admin")
				{
					return RedirectToAction("AdminDashboard", "Admin");
				}
				else
				{
					return RedirectToAction("Menu", "Home");
				}
			}
			else
			{
				Console.WriteLine("You don't exist, gurl.\nPlease sign up first.\n");
				ViewBag.ErrorMessage = "Invalid credentials, please sign up first.";
				return View();
			}
		}

		// GET: /User/SignUp
		public IActionResult SignUp()
		{
			return View();
		}

		// POST: /User/SignUp
		[HttpPost]
		public IActionResult SignUp(User user)
		{
			// Check if the user already exists
			bool userExists = _userService.AuthenticateUser(user.Email, user.Password);

			if (userExists)
			{
				Console.WriteLine("This login Id already exists\n");
				ViewBag.ErrorMessage = "This user already exists.";
				return View(user);
			}
			else
			{
				Console.WriteLine("Adding to database\n");
				_userService.PrepareSignupUser(user); // Prepare user data
				_userService.AddUser(user); // Add user to the database

				return RedirectToAction("Index", "Home");
			}
		}
	}
}
