using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class BookOrder
    {
        [Key, Column(Order = 0)]
        public int BookID { get; set; }
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }

        public virtual Book book { get; set; }
        public virtual Order order { get; set; }

        public int Quantity { get; set; }
    }
}
