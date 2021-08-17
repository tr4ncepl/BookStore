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

        [HttpPost]
        public ViewResult AddReview(BookDetailsViewModel model)
        {
            Book book = repository.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.BookID == model.Book.BookID);

            var review = new BookReview
            {
                BookRating = 4,
                PublicationDate = DateTime.Now,
                ReviewAuthor = model.ReviewAuthor,
                ReviewDesc = model.ReviewDesc
            };

            repository.AddReview(review, model.Book.BookID);

            var nmodel = new BookDetailsViewModel
            {
                Book = book
            };
            return View("BookDetails", nmodel);
        }

        public ViewResult BooksByAuthor(int  authorId , string authorName, int page = 1)
        {
            Author author = repository.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            BooksByAuthorViewModel model = new BooksByAuthorViewModel
            {
                Books = repository.Books
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b=>b.Genre)
                .Where(b => b.Author.AuthorId == authorId)
                .OrderBy(b => b.BookID)
                .Skip((page - 1) * PageSize)
                .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = author.AuthorName == null  ?
                        repository.Books.Count() :
                        repository.Books.Where(e => e.Author.AuthorName == authorName).Count()
                },
                Author=author,
                CurrentAuthor=author.AuthorName
                
            };

            return View(model);
        }

        public ViewResult List(string genre, int page =1)
        {
            BookListViewModel model = new BookListViewModel
            {
                Books = repository.Books
                .Include(b=>b.Publisher)
                .Include(b=>b.Genre)
                .Include(b=>b.Author)
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



        public ViewResult BookDetails(int bookId)
        {
            Book book = repository.Books
                .Include(b => b.Publisher)
                .Include(b => b.Author)
                .Include(b => b.Genre)
                .FirstOrDefault(b => b.BookID == bookId);

            var model = new BookDetailsViewModel
            {
                Book = book
            };

            return View(model);
        }


       
        
    }
}