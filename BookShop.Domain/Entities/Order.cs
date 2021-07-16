using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Order
    {

        public int OrderId { get; set; }
        public string RecName { get; set; }
        public string RecAdress { get; set; }
        public string RecCity { get; set; }
        public string RecState { get; set; }
        public string RecZip { get; set; }
        public string RecCountry { get; set; }
        public bool GiftWrap { get; set; }

        public decimal TotalValue { get; set; }

        public virtual ICollection<BookOrder> BookOrders { get; set; }

    }
}
