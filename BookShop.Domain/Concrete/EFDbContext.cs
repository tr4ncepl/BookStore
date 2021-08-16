using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using BookShop.Domain.Entities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace BookShop.Domain.Concrete
{
    public class EFDbContext : DbContext
    {

        public EFDbContext() : base("EFDbContext") { }
        public DbSet<Book> Books { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<Author> Authors { get; set; }

        public DbSet<BookOrder> BookOrders { get; set; }

        public DbSet<Publisher> Publishers { get; set; }

        public DbSet<Genre> Genres { get; set; }

        public DbSet<BookReview> BookReviews { get; set; }

        

    }
}
