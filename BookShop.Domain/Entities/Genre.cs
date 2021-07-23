using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Genre
    {

        public int GenreId { get; set; }


        [Display(Name = "Gatunek")]
        public string GenreName { get; set; }
    }
}
