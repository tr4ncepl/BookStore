using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;
using BookShop.WebUI.Models;
using System.Data.Entity;

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

        public ViewResult List(string genre, int page =1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books
                .Include(b=>b.Publisher)
                .Include(b=>b.Genre)
                .Where(b => genre == null || b.Genre.GenreName == genre)
                .OrderBy(b => b.BookID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = genre ==null?
                        repository.Books.Count() :
                        repository.Books.Where(e=>e.Genre.GenreName==genre).Count()
                },
                CurrentGenre = genre
            };
            return View(model);
        }

        public FileContentResult GetImage(int bookId)
        {
            Book bk = repository.Books.FirstOrDefault(b => b.BookID == bookId);
            if(bk!=null)
            {
                return File(bk.ImageData, bk.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        
    }
}