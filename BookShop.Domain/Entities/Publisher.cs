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

        [Display(Name ="Nazwa wydawnictwa")]
        public string PublisherName { get; set; }

        [DataType(DataType.MultilineText), Display(Name = "Opis wydawnictwa")]
        [Required]
        public string PublisherDescription { get; set; }

    }
}
