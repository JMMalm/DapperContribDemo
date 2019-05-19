using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace DapperContribDemo.MVC.Controllers
{
	public class UsersController : Controller
	{
		public IActionResult Index()
		{
			return View();
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