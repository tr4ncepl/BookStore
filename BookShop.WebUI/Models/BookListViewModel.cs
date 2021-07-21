using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShop.Domain.Concrete;
using BookShop.Domain.Entities;
using BookShop.WebUI.Models;

namespace BookShop.WebUI.Models
{
    public class BookListViewModel
    {
        public IQueryable<Book> Books { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentGenre { get; set; }
    }


    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }



    public class TestViewModel
    {

        public TestViewModel()
        {
            this.Books = new Book();
        }

        private EFDbContext context = new EFDbContext();
        public IEnumerable<int> SelectedItemIds { get; set; }

        public IEnumerable<int> SelectedAuthors { get; set; }

        public Book Books { get; set; }
        public IEnumerable<Publisher> AvailableItems
        {
            get { return context.Publishers; }
        }

        public IEnumerable<Author> AvailableAuthors
        {
            get { return context.Authors; }
        }
    }
}
