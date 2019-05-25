using DapperContribDemo.Core;
using DapperContribDemo.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

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

		public async Task<IActionResult> Index(string searchTerm)
		{
			var users = _userRepo.GetUsers();

			if (!string.IsNullOrWhiteSpace(searchTerm))
			{
				users = users.Where(u =>
					u.FirstName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase)
					|| u.LastName.Contains(searchTerm, StringComparison.InvariantCultureIgnoreCase));
			}

			return await Task.Run(() => View(users));
		}

		[HttpGet]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(User user)
		{
			if (ModelState.IsValid)
			{
				_userRepo.InsertUser(user);
				return RedirectToAction("Index");
			}
			return View(user);
		}

		public IActionResult Edit(int? id)
		{
			if (id == null || id < 0)
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
		public IActionResult Edit([Bind("FirstName, LastName, Email")] User user)
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