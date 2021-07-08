using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}