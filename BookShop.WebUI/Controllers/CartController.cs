using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;
using BookShop.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.WebUI.Controllers
{
    public class CartController : Controller
    {
        private IBookRepository repository;

        public CartController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string returUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = GetCart(),
                ReturnUrl = returUrl
            });
        }

        public RedirectToRouteResult AddToCart(int bookId, string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);

            if(book!=null)
            {
                GetCart().AddItem(book, 1);
            }
            return RedirectToAction("Index", new { returnUrl });
        }


        public RedirectToRouteResult RemoveFromCart(int bookId,string returnUrl)
        {
            Book book = repository.Books
                .FirstOrDefault(b => b.BookID == bookId);

            if(book!=null)
            {
                GetCart().RemoveLine(book);
            }
            return RedirectToAction("Index", new { returnUrl });
        }

        private Cart GetCart()
        {
            Cart cart = (Cart)Session["Cart"];
            if(cart==null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }

            return cart;
        }
    }
}