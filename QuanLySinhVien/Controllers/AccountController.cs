using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using QuanLySinhVien.Helper;
using QuanLySinhVien.Data;
using QuanLySinhVien.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace CookieDemo.Controllers
{
    public class AccountController : Controller
    {
        public readonly QLSVContext _context;
        public readonly IAccountHelper _accountHelper;

        public AccountController(QLSVContext context, IAccountHelper accountHelper)
        {
            _context = context;
            _accountHelper = accountHelper;
        }

        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string UserName, string password)
        {
            if(!string.IsNullOrEmpty(UserName) && string.IsNullOrEmpty(password))
            {
                return RedirectToAction("Login");
            }
            Account data = _accountHelper.GetAccountByLogin(UserName, password);// kiểm tra có tài khoản hay không          
            ClaimsIdentity Identity = null;
            bool IsAuthenticated = false;

            if (data != null)
            {
                if (data.PermissionID == 1)
                {
                    Identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, data.UserName),
                    new Claim(ClaimTypes.Role, "Admin")
                
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    IsAuthenticated = true;
                }
                else
                {
                    Identity = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, data.UserName),
                    new Claim(ClaimTypes.Role, "User")
              
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                    IsAuthenticated = true;
                }
                if (IsAuthenticated)
                {
                    var principal = new ClaimsPrincipal(Identity);

                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    return RedirectToAction("Index", "Home");
                }

            }
           
            return View();
        }

        
       
        public IActionResult Logout()
        {
            var login = HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login");
        }
    }
}


