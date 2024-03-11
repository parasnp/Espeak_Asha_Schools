using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using ParasIspeak.Data;
using ParasIspeak.Models;
namespace ParasIspeak.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AccountController(ApplicationDbContext db,SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager,UserManager<IdentityUser>userManager)
        {
            _db = db;
            _signInManager=signInManager;
            _roleManager=roleManager;
            _userManager=userManager;

        }
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            LoginVM vm = new() { RedirectUrl = returnUrl };
            return View(vm);
        }
        public async Task<IActionResult> Register()
        
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole("Admin"));
                await _roleManager.CreateAsync(new IdentityRole("User"));
            }
            RegisterVM vm = new()
            {
                RolesList=_roleManager.Roles.Select(x=>new SelectListItem{
                    Text=x.Name,
                    Value=x.Name
                })
            };
            return View(vm);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM vm)
        {
            if (ModelState.IsValid)
            {
                var EmailExists = _db.Users.Any(x => x.Email == vm.Email);
                if (EmailExists)
                {
                    ModelState.AddModelError("Email", "User with this error already exists");
                    return View(vm);
                }
            }
            IdentityUser myuser = new() 
            {
                UserName = vm.Email,
                Email = vm.Email,
                EmailConfirmed = true,
                NormalizedEmail = vm.Email.ToUpper()   
            };
            var result = await _userManager.CreateAsync(myuser,vm.password);
            if (result.Succeeded)
            {
                if (!string.IsNullOrEmpty(vm.SelectedRole))
                {
                    await _userManager.AddToRoleAsync(myuser, vm.SelectedRole);
                }
                else
                { await _userManager.AddToRoleAsync(myuser, "User"); }
                return RedirectToAction ("Privacy","Home");
            }
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            vm.RolesList = _roleManager.Roles.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Name
            });
            return View(vm);

        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM lvm)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(lvm.Email, lvm.PassWord, lvm.Rememberme, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(lvm.Email);
                    if (user != null)
                    {
                        var isAdmin = await _userManager.IsInRoleAsync(user, "Admin");
                        if (isAdmin)
                        { return RedirectToAction("Index", "AdminDash"); }
                        else 
                        { return RedirectToAction("Index", "ImageReader"); }
                    }
                }
            }
            return RedirectToAction("Index", "ImageReader");

        }
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
  
}

