using InfobridgeEx.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InfobridgeEx.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(Admin obj)
        {
            if (obj.Username == "Admin" && obj.Password == "Admin123")
            {
                return RedirectToAction("Index", "Home");
            }
            return View("login");


        }
    }
}
