using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Book
    {
        public int BookID { get; set; }
        public string Title { get; set; }

        public string Author { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public decimal Rating { get; set; }
    }
}
