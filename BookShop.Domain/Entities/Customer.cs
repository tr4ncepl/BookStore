using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class Customer : AppUser
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
    }
}
