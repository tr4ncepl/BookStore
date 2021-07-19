using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Order
    {

        public int OrderId { get; set; }
        [Display(Name = "Imię i nazwisko")]
        public string RecName { get; set; }
        [Display(Name = "Ulica")]
        public string RecAdress { get; set; }
        [Display(Name = "Miasto")]
        public string RecCity { get; set; }
        [Display(Name = "Województwo")]
        public string RecState { get; set; }
        [Display(Name = "Kod pocztowy")]
        public string RecZip { get; set; }
        [Display(Name = "Kraj")]
        public string RecCountry { get; set; }
        [Display(Name = "Ozdobne pakowanie")]
        public bool GiftWrap { get; set; }

        [Display(Name = "Całkowita wartość zamówienia")]
        public decimal TotalValue { get; set; }

        public virtual ICollection<BookOrder> BookOrders { get; set; }

    }
}
