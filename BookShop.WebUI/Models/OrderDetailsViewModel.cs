using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BookShop.WebUI.Models
{
    public class OrderDetailsViewModel
    {
        [Display(Name = "ID Zamówienia")]
        public int OrderId { get; set; }
        

        [Display(Name = "Imię Nazwisko")]
        public string Name { get; set; }

        [Display(Name = "Ulica")]
        public string Adress { get; set; }

        [Display(Name = "Miasto")]
        public string City { get; set; }

       
        [Display(Name="Województwo")]
        public string State { get; set; }

        [Display(Name = "Kod Pocztowy")]
        public string ZipCode { get; set; }

        [Display(Name = "Kraj")]
        public string Country { get; set; }


        [Display(Name="Ozdobne Pakowanie")]
        public Boolean GiftWrap { get; set; }

        public decimal TotalValue { get; set; }

        public IEnumerable<BookOrder> BooksInOrder { get; set; }

    }

    public class AddBooksToOrderViewModel
    {
        [Required(ErrorMessage = "Proszę podać dodatnią cenę.")]
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }

        public int OrderId { get; set; }

        public IEnumerable<int> SelectedBookId { get; set; }


        public IEnumerable<Book> AvailableBooks { get; set; }
    }
}