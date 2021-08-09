using BookShop.Domain.Concrete;
using BookShop.Domain.Entities;
using BookShop.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace BookShop.WebUI.Controllers
{
    
    public class AccountController : Controller
    {
        // GET: Account
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if(HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Brak dostępu" });
            }
            ViewBag.returnUrl = returnUrl;
            return View();

        }


        
        public ActionResult Register()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<ActionResult> Register(CreateCustomerModel model)
        {
            if (ModelState.IsValid)
            {
                
                Customer customer = new Customer { UserName = model.UserName, Name = model.Name, Email = model.email, LastName =model.LastName, Address = model.Address };
                IdentityResult result = await UserManager.CreateAsync(customer,
                model.password);
                if (result.Succeeded)
                {
                    return RedirectToAction("List","Book", new { area = "" });
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(model);
        }






        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (string error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }



        [Authorize]
        public ActionResult Logout()
        {
            AuthManager.SignOut();
            return RedirectToAction("..");
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel details, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                AppUser user = await UserManager.FindAsync(details.Name, details.Password);
                if(user==null)
                {
                    ModelState.AddModelError("", "Zły login lub hasło");
                }
                else
                {
                    ClaimsIdentity ident = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
                    AuthManager.SignOut();
                    AuthManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = false
                    }, ident);
                    return Redirect(returnUrl);
                }
            }
            ViewBag.returnUrl = returnUrl;
            return View(details);
        }

        private IAuthenticationManager AuthManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }
    }
}