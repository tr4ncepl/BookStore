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

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }




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
                AppUser user = new AppUser { UserName = model.Name, Email = model.Email};
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
        public async Task<ActionResult> EditUser(string id, string email, string password, string test)
        {
            AppUser user = await UserManager.FindByIdAsync(id);
            if(user!=null)
            {
                user.Email = email;
                user.Test = test;
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
                .Include(b=>b.Genre)
                .Include(b=>b.Author)
                .FirstOrDefault(b => b.BookID == bookId);
            var autId = book.Author.AuthorId;
            var pubId = book.Publisher.PublisherId;
            var genId = book.Genre.GenreId;
            var model = new TestView
            {
                BookID=book.BookID,
                Title=book.Title,
                Rating=book.Rating,
               
                Description=book.Description,
                Price=book.Price,
                ImageData=book.ImageData,
                ImageMimeType=book.ImageMimeType,
                SelectedItemIds = new[] { pubId },
                SelectedAuthors = new[] { autId },
                SelectedGenres = new[] {genId}

            };
            return View(model);
        }

       

        [Authorize(Roles =("admin,superadmin"))]
        public ViewResult EditOrder(int orderId)
        {
            Order order = repository.Orders
                .FirstOrDefault(o => o.OrderId == orderId);

            var query = repository.BookOrders
                .Where(book => book.order.OrderId == orderId);

            var total = query.Sum(e => e.book.Price * e.Quantity);

            var model = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                Adress = order.RecAdress,
                Name = order.RecName,
                City = order.RecCity,
                Country = order.RecCountry,
                State = order.RecState,
                ZipCode = order.RecZip,
                BooksInOrder = query,
                TotalValue = total
            };
            return View(model);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult EditOrder(OrderDetailsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var order = new Order
                {
                    OrderId = model.OrderId,
                    RecAdress = model.Adress,
                    RecCity = model.City,
                    RecName = model.Name,
                    RecCountry = model.Country,
                    RecState = model.State,
                    RecZip = model.ZipCode,
                    TotalValue = model.TotalValue,
                    GiftWrap = model.GiftWrap,
                    BookOrders = (ICollection<BookOrder>)model.BooksInOrder
                };

                repository.SaveOrder(order);
                TempData["message"] = string.Format("Zaktualizowano zamówienie nr: {0}", order.OrderId);
                return RedirectToAction("OrderList");
            }
            else
            {
                return View(model);
            }
        }




        



       

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult Edit(IEnumerable<int> selectedItemIds, TestView book, IEnumerable<int> selectedAuthors,IEnumerable<int> selectedGenres, HttpPostedFileBase image = null)
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
                int gen = selectedGenres.First();
                var bk = new Book
                {
                    BookID = book.BookID,
                    Title = book.Title,
                    Description = book.Description,
                    Price = book.Price,
                    Rating = book.Rating,
                    ImageData = book.ImageData,
                    ImageMimeType = book.ImageMimeType,

                };
                repository.SaveBook(bk, pub, aut,gen);
                TempData["message"] = string.Format("Zapisano {0} ", bk.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }

            


        }

        [Authorize(Roles =("admin,superadmin"))]
        public ViewResult NewBookInOrder(int orderId)
        {
            var books = repository.Books;
            var model = new AddBooksToOrderViewModel
            {
                SelectedBookId = new[] { 1 },
                AvailableBooks = books,
                OrderId = orderId
                
            };
            return View("EditBookInOrder", model);
        }

        
        
        [HttpPost]
        public ActionResult EditBookInOrder(IEnumerable<int> SelectedBookId, int quantity, int orderId)
        {
            var model = GetOrderModel(orderId);

            if (ModelState.IsValid)
            {
                var book = repository.Books
                    .FirstOrDefault(b => b.BookID == SelectedBookId.FirstOrDefault());
                var order = repository.Orders
                    .FirstOrDefault(o => o.OrderId == orderId);
                BookOrder bookOrder = new BookOrder
                {
                    Quantity = quantity,
                    book = book,
                    order = order
                };

                repository.AddBookToOrder(bookOrder);
                TempData["message"] = string.Format("Dodano nowy przedmiot do zamówienia");


                return RedirectToAction("EditOrder",model);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        



        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult Create()
        {
            var model = new TestView
            {
                
                SelectedItemIds = new[] { 1 },
                SelectedAuthors = new[] { 1 },
                SelectedGenres = new[] {1}

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
                TempData["message"] = string.Format("Zapisano {0} ", author.AuthorName);
                return RedirectToAction("AuthorsList");
            }
            else
            {
                return View(author);
            }
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult DeleteAuthor(int authorId)
        {
            Author deletedAuthor = repository.DeleteAuthor(authorId);
            if(deletedAuthor!=null)
            {
                TempData["message"] = string.Format("Usunięto {0}", deletedAuthor.AuthorName);
            }
            return RedirectToAction("AuthorsList");
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


        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult DeletePublisher(int publisherId)
        {
            Publisher deletedPublisher = repository.DeletePublisher(publisherId);
            if(deletedPublisher!=null)
            {
                TempData["message"] = string.Format("Pomyślnie usunięto wydawnictwo {0}", deletedPublisher.PublisherName);
            }
            return RedirectToAction("PublisherList");
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult GenresList()
        {
            return View(repository.Genres);
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult NewGenre()
        {
            return View("EditGenre", new Genre());
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult EditGenre(int genreId)
        {
            Genre genre = repository.Genres
                .FirstOrDefault(g => g.GenreId == genreId);
            return View(genre);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        [HttpPost]
        public ActionResult EditGenre(Genre genre)
        {
            if(ModelState.IsValid)
            {
                repository.SaveGenre(genre);
                TempData["message"] = string.Format("Pomyślnie zapisano gatunek: {0}", genre.GenreName);
                return RedirectToAction("GenresList");
            }
            else
            {
                return View(genre);
            }
        }


        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult DeleteGenre(int genreId)
        {
            Genre deletedGenre = repository.DeleteGenre(genreId);
            if(deletedGenre!=null)
            {
                TempData["message"] = string.Format("Pomyślnie usunięto: {0}", deletedGenre.GenreName);
                
            }
            return RedirectToAction("GenresList");

        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ActionResult DeleteOrder(int orderId)
        {
            Order deletedOrder = repository.DeleteOrder(orderId);
            if(deletedOrder!=null)
            {
                TempData["message"] = string.Format("Pomyślnie usunięto zamowienie nr:  {0}", deletedOrder.OrderId);
            }
            return RedirectToAction("OrderList");
        }


        public OrderDetailsViewModel GetOrderModel (int orderId)
        {
            Order order = repository.Orders
                .FirstOrDefault(f => f.OrderId == orderId);

            var query = repository.BookOrders
                .Where(book => book.order.OrderId == orderId);

            var total = query.Sum(e => e.book.Price * e.Quantity);

            var model = new OrderDetailsViewModel
            {
                OrderId = order.OrderId,
                Adress = order.RecAdress,
                Name = order.RecName,
                City = order.RecCity,
                Country = order.RecCountry,
                State = order.RecState,
                ZipCode = order.RecZip,
                BooksInOrder = query,
                GiftWrap = order.GiftWrap,
                TotalValue = total



            };

            return model;
        }

        [Authorize(Roles =("admin,superadmin"))]
        public ActionResult DeleteBookInOrder(int orderId, int bookId)
        {
            BookOrder deletedBook = repository.DeleteBookInOrder(orderId, bookId);
            if(deletedBook!=null)
            {
                TempData["message"] = string.Format("Pomyślnie usunięto książkę z zamówienia");
            }

            var model = GetOrderModel(orderId);



            return RedirectToAction("EditOrder", model);
        }

        [Authorize(Roles = ("admin,superadmin"))]
        public ViewResult OrderDetails(int orderId)
        {
            Order order = repository.Orders
                .FirstOrDefault(f => f.OrderId == orderId);

            var query = repository.BookOrders
                .Where(book => book.order.OrderId == orderId);

            var total = query.Sum(e => e.book.Price * e.Quantity);

            var model = new OrderDetailsViewModel
            {
                OrderId=order.OrderId,
                Adress = order.RecAdress,
                Name = order.RecName,
                City = order.RecCity,
                Country = order.RecCountry,
                State = order.RecState,
                ZipCode = order.RecZip,
                BooksInOrder=query,
                GiftWrap=order.GiftWrap,
                TotalValue=total



            };

            return View(model);


        }
















    }
}