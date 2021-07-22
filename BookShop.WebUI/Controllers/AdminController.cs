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
using System.Data.Entity;
using System.Dynamic;
using System.Text;

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

       

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        

       

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult Index()
        {
            var result = repository.Books.Include(b => b.Publisher);
            return View(result);
            
        }

        [Authorize(Roles =("admin,superadmin"))]
        public ViewResult OrderList()
        {
            return View(repository.Orders);
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
            var book = repository.Books.Include(b=>b.Publisher)
                .Include(b=>b.Author)
                .FirstOrDefault(b => b.BookID == bookId);
            var autId = book.Author.AuthorId;
            var pubId = book.Publisher.PublisherId;
            var model = new TestView
            {
                BookID=book.BookID,
                Title=book.Title,
                Rating=book.Rating,
                Genre = book.Genre,
                Description=book.Description,
                Price=book.Price,
                ImageData=book.ImageData,
                ImageMimeType=book.ImageMimeType,
                SelectedItemIds = new[] { pubId },
                SelectedAuthors = new[] { autId }

            };
            return View(model);
        }

       

        [Authorize(Roles =("admin,superadmin"))]
        public ViewResult EditOrder(int orderId)
        {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderId == orderId);
            return View(order);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult EditOrder(Order order)
        {
            if (ModelState.IsValid)
            {
                repository.SaveOrder(order);
                TempData["message"] = string.Format("Zapisano zamówienie nr {0}", order.OrderId);
                return RedirectToAction("OrderList");
            }
            else
            {
                return View(order);
            }
        }



       

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult Edit(IEnumerable<int> selectedItemIds, TestView book, IEnumerable<int> selectedAuthors, HttpPostedFileBase image = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    book.ImageMimeType = image.ContentType;
                    book.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(book.ImageData, 0, image.ContentLength);
                }
                

                int aut = selectedAuthors.First();
                int pub = selectedItemIds.First();
                var bk = new Book
                {
                    BookID = book.BookID,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Rating = book.Rating,
                    Genre = book.Genre,
                    ImageData = book.ImageData,
                    ImageMimeType = book.ImageMimeType,

                };
                repository.SaveBook(bk, pub, aut);
                TempData["message"] = string.Format("Zapisano {0} ", bk.Title);
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
            var model = new TestView
            {
                
                SelectedItemIds = new[] { 1 },
                SelectedAuthors = new[] { 1 }

            };
            return View("Edit", model);
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









        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult AuthorsList()
        {
            return View(repository.Authors);
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult NewAuthor()
        {
            return View("EditAuthor", new Author());
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult EditAuthor(int authorId)
        {
            Author author = repository.Authors
                .FirstOrDefault(a => a.AuthorId == authorId);
            return View(author);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult EditAuthor(Author author)
        {
            if(ModelState.IsValid)
            {
                repository.SaveAuthor(author);
                TempData["message"] = string.Format("Zapisano {0} ", author.AuthorName + " " + author.AuthorLastName);
                return RedirectToAction("AuthorsList");
            }
            else
            {
                return View(author);
            }
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult PublisherList()
        {
            return View(repository.Publishers);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult NewPublisher()
        {
            return View("EditPublisher", new Publisher());
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult EditPublisher(int publisherId)
        {
            Publisher publisher = repository.Publishers
                .FirstOrDefault(p => p.PublisherId == publisherId);
            return View(publisher);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult EditPublisher(Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                repository.SavePublisher(publisher);
                TempData["message"] = string.Format("Pomyślnie zapisano wydawcę: {0}", publisher.PublisherName);
                return RedirectToAction("PublisherList");
            }
            else
            {
                return View(publisher);
            }
        }
















    }
}