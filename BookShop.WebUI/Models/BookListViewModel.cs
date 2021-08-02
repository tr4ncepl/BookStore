using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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

    public class BooksByAuthorViewModel
    {
        public IQueryable<Book> Books { get; set; }

        public PagingInfo PagingInfo { get; set; }

        public Author Author { get; set; }

        public string CurrentAuthor { get; set; }
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

    public class TestView 
    {
        [HiddenInput(DisplayValue = false)]
        public int BookID { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę książki.")]
        [Display(Name = "Tytuł")]
        public string Title { get; set; }


        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Proszę podać dodatnią cenę.")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Opis")]
        [Required(ErrorMessage = "Proszę podać opis.")]
        public string Description { get; set; }

        [Display(Name = "Ocena")]
        [Required]
        [Range(0, 10, ErrorMessage = "Proszę podać ocenę 0-10")]
        public decimal Rating { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }


        private EFDbContext context = new EFDbContext();
        public IEnumerable<int> SelectedItemIds { get; set; }

        public IEnumerable<int> SelectedAuthors { get; set; }

        public IEnumerable<int> SelectedGenres { get; set; }


        public IEnumerable<Publisher> AvailableItems
        {
            get { return context.Publishers; }
        }

        public IEnumerable<Author> AvailableAuthors
        {
            get { return context.Authors; }
        }

        public IEnumerable<Genre> AvailableGenres
        {
            get { return context.Genres; }
        }
    }



}
