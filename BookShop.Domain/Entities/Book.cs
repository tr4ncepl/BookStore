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

        [Required(ErrorMessage ="Proszę podać nazwę książki.")]
        [Display(Name ="Tytuł")]
        public string Title { get; set; }

        [Required]
        [Range(0.01, double.MaxValue,ErrorMessage ="Proszę podać dodatnią cenę.")]
        [Display(Name ="Cena")]
        public decimal Price { get; set; }

        [Required]
        [Range(10, int.MaxValue, ErrorMessage = "Proszę podać dodatnią ilosc stron.")]
        [Display(Name = "Ilość stron")]
        public int PagesNumber { get; set; }

        [DataType(DataType.MultilineText),Display(Name ="Opis")]
        [Required(ErrorMessage ="Proszę podać opis.")]
        public string Description { get; set; }

        [Display(Name ="Ocena")]
        [Required]
        [Range(0,10,ErrorMessage ="Proszę podać ocenę 0-10")]
        public decimal Rating { get; set; }

        public byte[] ImageData { get; set; }
        public string ImageMimeType { get; set; }
        

        [Display(Name = "Autor")]
       
        public Author Author { get; set; }

        [Display(Name = "Wydawca")]
        
        public Publisher Publisher { get; set; }

        [Display(Name ="Gatunek")]
        public Genre Genre { get; set; }



        public virtual ICollection<BookOrder> BookOrders { get; set; }
    }
}
