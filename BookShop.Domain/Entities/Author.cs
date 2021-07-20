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
        [Display(Name="Imie")]
        public string AuthorName { get; set; }
        [Display(Name ="Nazwisko")]
        public string AuthorLastName { get; set; }
    }
}
