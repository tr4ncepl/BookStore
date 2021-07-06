using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;

namespace BookShop.WebUI.Controllers
{
    public class BookController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 4;

        public BookController(IBookRepository bookRepository)
        {
            this.repository = bookRepository;
        }

        public ViewResult List(int page =1)
        {
            return View(repository.Books
                .OrderBy(b=>b.BookID)
                .Skip((page-1)*PageSize)
                .Take(PageSize));
        }
        
    }
}