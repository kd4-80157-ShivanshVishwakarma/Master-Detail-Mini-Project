using MasterDetailProject.Models;
using MasterDetailProject.Repository;
using Microsoft.AspNetCore.Mvc;

namespace MasterDetailProject.Controllers
{
    public class AccountController : Controller
    {
        AccountRepository service;
        public AccountController()
        {
            service = new AccountRepository();
        }

        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Create(Signup value)
        {
            bool flag = service.SignUp(value.Name, value.Password, value.Email, value.Phone, value.Address);
            if (flag)
            {
                return RedirectToAction("Login");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Signup failed. Please try again.");
                return View();
            }

        }

        public IActionResult Validate(Login value)
        {
            bool flag = service.SignIn(value);
            if (flag)
            {
                return RedirectToAction("GetList","Product");
            }
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }
    }
}
