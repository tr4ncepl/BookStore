using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Publisher
    {

        public int PublisherId { get; set; }
        
        [Display(Name ="Nazwa")]
        [Required]
        public string PublisherName { get; set; }

        [Display(Name = "Opis")]
        [Required]
        public string PublisherDesc { get; set; }
    }
}
