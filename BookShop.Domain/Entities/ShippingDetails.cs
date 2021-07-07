using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BookShop.Domain.Entities
{
    public class ShippingDetails
    {
        [Required(ErrorMessage = "Proszę podać nazwisko.")]
        [Display(Name="Imię i nazwisko")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Proszę uzupełnić adres.")]
        [Display(Name="Wiersz 1")]
        public string Line1 { get; set; }
        [Display(Name="Wiersz 2")]
        public string Line2 { get; set; }

        [Required(ErrorMessage ="Proszę podać nazwę miasta")]
        [Display(Name="Miasto")]
        public string City { get; set; }

        [Required(ErrorMessage = "Proszę podać nazwę wojwewództwa")]
        [Display(Name="Województwo")]
        public string State { get; set; }

        [Required(ErrorMessage = "Proszę podać kod pocztowy")]
        [Display(Name="Kod pocztowy")]
        public string Zip { get; set; }

        [Required(ErrorMessage ="Proszę podać nazwę kraju")]
        [Display(Name="Kraj")]
        public string Country { get; set; }

        public bool GiftWrap { get; set; }
    }
}
