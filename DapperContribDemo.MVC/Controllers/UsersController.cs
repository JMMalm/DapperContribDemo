using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Configuration;
using DapperContribDemo.Infrastructure.Repositories;
using DapperContribDemo.Core;

namespace DapperContribDemo.MVC.Controllers
{
	public class UsersController : Controller
	{
		private readonly IConfiguration _config;
		private readonly UserRepository _userRepo;

		public UsersController(IConfiguration config)
		{
			if (config == null)
			{
				throw new ArgumentNullException("Configuration cannot be null.");
			}
			_config = config;
			_userRepo = new UserRepository(_config);
		}

		public IActionResult Index()
		{
			return View(_userRepo.GetUsers());
		}

		public IActionResult Edit(int? id)
		{
			if (id == null && id < 0)
			{
				return NotFound();
			}

			var user = _userRepo.GetUser((int)id);
			if (user == null)
			{
				return NotFound();
			}
			return View(user);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(User user)
		{
			if (ModelState.IsValid)
			{
				try
				{
					_userRepo.UpdateUser(user);
					return RedirectToAction(nameof(Index));
				}
				catch (Exception)
				{
					throw;
				}
			}
			return View(user);
		}

		public IActionResult Welcome(string name)
		{
			if (string.IsNullOrWhiteSpace(name))
			{
				ViewData["Message"] = "Welcome from the Users controller!";
			}
			else
			{
				ViewData["Message"] = HtmlEncoder.Default.Encode($"Hello {name}!");
			}

			return View();
		}
	}
}