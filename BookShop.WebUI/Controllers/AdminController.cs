using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;

using BookShop.Domain.Abstract;

namespace BookShop.WebUI.Controllers
{
    public class AdminController : Controller
    {

        private IBookRepository repository;
        // GET: Admin

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index()
        {
            return View(repository.Books);
        }

        public ViewResult Edit(int bookId)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);
            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            if(ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = string.Format("Zapisano {0} ", book.Title);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new Book());
        }
    }
}