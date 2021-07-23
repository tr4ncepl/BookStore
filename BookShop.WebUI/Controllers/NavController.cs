using BookShop.Domain.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace BookShop.WebUI.Controllers
{
    public class NavController : Controller
    {
        private IBookRepository repository;

        public NavController(IBookRepository repo)
        {
            repository = repo;
        }

        public PartialViewResult Menu(string genre =null)
        {
            ViewBag.SelectedGenre = genre;
            IEnumerable<string> genres = repository.Books
                .Include(g=>g.Genre)
                .Select(b => b.Genre.GenreName)
                .Distinct()
                .OrderBy(x => x);

            return PartialView(genres);
        }
    }
}