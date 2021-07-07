using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BookShop.Domain.Entities;

namespace BookShop.WebUI.Models
{
    public class BookListViewModel
    {
        public IEnumerable<Book> Books { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public string CurrentGenre { get; set; }
    }
}