using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookShop.Domain.Entities;

namespace BookShop.Domain.Concrete
{
    class EFDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
    }
}
