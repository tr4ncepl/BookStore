using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }
        [Display(Name="Imię i nazwisko")]
        public string AuthorName { get; set; }
        [Display(Name ="Opis")]
        public string AuthorDesc { get; set; }
    }
}
