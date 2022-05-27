using System.Threading.Tasks;
using AppWithDocker.Data_DbContext;
using AppWithDocker.Models;
using AppWithDocker.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AppWithDocker.Controllers
{
    public class AccountController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly AppDBContext _appDBContext;


        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, AppDBContext dBContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _appDBContext = dBContext;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDetails
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Password = model.Password,
                    Mobile = model.Mobile
                };

                var result =_appDBContext.Add(user);
                await _appDBContext.SaveChangesAsync();

                if (result != null)
                    return RedirectToAction("Login", "Account");
                else
                    ModelState.AddModelError(string.Empty, "Invalid Registration Attempt");

            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            if (ModelState.IsValid)
            {
                var result = await _appDBContext.Userdetails.SingleOrDefaultAsync(
                    x => x.Email == user.Email && x.Password == user.Password); 

                if (result != null)
                {
                    HttpContext.Session.SetString("userId", result.UserName);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

            }
            return View(user);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

    }
}
