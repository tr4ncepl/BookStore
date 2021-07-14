using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;
using BookShop.Domain.Concrete;
using BookShop.Domain.Abstract;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Owin;
using Microsoft.AspNet.Identity.Owin;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private IBookRepository repository;
        

        [Authorize(Roles =("admin,superadmin"))]
        public ActionResult UserList()
        {
            return View(UserManager.Users);
        }

        private AppUserManager UserManager
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
            }
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult CreateUser()
        {
            return View();
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public async Task<ActionResult> CreateUser(CreateModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email };
                IdentityResult result = await UserManager.CreateAsync(user,
                model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("UserList");
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

        // GET: Admin

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult Index()
        {
            return View(repository.Books);
        }


        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public async Task<ActionResult> DeleteUser(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                IdentityResult result = await UserManager.DeleteAsync(user);
                if(result.Succeeded)
                {
                    return RedirectToAction("UserList");
                }
                else
                {
                    return View("Error", result.Errors);
                }
            }
            else
            {
                return View("Error", new string[] { "Nie znaleziono użytkownika" });
            }
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public async Task<ActionResult> EditUser(string id)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                return View(user);
            }
            else
            {
                return RedirectToAction("UserList");
            }
        }


        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public async Task<ActionResult> EditUser(string id, string email, string password)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                user.Email = email;
                IdentityResult validEmail = await UserManager.UserValidator.ValidateAsync(user);
                if(!validEmail.Succeeded)
                {
                    AddErrorsFromResult(validEmail);
                }
                IdentityResult validPass = null;
                if(password!=string.Empty)
                {
                    validPass = await UserManager.PasswordValidator.ValidateAsync(password);
                    if(validPass.Succeeded)
                    {
                        user.PasswordHash = UserManager.PasswordHasher.HashPassword(password);
                    }
                    else
                    {
                        AddErrorsFromResult(validPass);
                    }
                }
                if((validEmail.Succeeded && validPass==null) || (validEmail.Succeeded && password!=string.Empty && validPass.Succeeded))
                {
                    IdentityResult result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("UserList");
                    }
                    else
                    {
                        AddErrorsFromResult(result);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Nie znaleziono użytkownika");
            }
            return View(user);
        }



        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult Edit(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);
            return View(book);
        }


        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult Edit(Book book, HttpPostedFileBase image=null)
        {
            if(ModelState.IsValid)
            {
                if(image!=null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                repository.SaveBook(book);
                TempData["message"] = string.Format("Zapisano {0} ", book.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult Create()
        {
            return View("Edit", new Book());
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult Delete(int bookID)
        {
            Book deletedBook = repository.DeleteBook(bookID);
            if(deletedBook!=null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedBook.Title);
            }
            return RedirectToAction("Index");
        }

        


        


    }
}