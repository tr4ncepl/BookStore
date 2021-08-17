using BookShop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookShop.WebUI.Models
{
    public class BookDetailsViewModel
    {
        public Book Book { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int BookReviewId { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Recenzja")]
        [Required(ErrorMessage = "Proszę podać recenzje.")]
        public string ReviewDesc { get; set; }


        [Required(ErrorMessage = "Proszę podać imię i nazwisko autora")]
        public string ReviewAuthor { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        
        [Range(0, 5, ErrorMessage = "Proszę podać dodatnią ilosc stron.")]
        [Display(Name = "Ilość stron")]
        public int BookRating { get; set; }

    }
}