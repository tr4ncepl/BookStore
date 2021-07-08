using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;


namespace BookShop.Domain.Entities
{
    public class Book
    {
        [HiddenInput(DisplayValue=false)]
        public int BookID { get; set; }
        
        [Display(Name ="Tytuł")]
        public string Title { get; set; }

        [Display(Name ="Autor")]
        public string Author { get; set; }

        [Display(Name ="Gatunek")]
        public string Genre { get; set; }
        
        [Display(Name ="Cena")]
        public decimal Price { get; set; }

        [DataType(DataType.MultilineText),Display(Name ="Opis")]
        public string Description { get; set; }

        [Display(Name ="Cena")]
        public decimal Rating { get; set; }
    }
}
