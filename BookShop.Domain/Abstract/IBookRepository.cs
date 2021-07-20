using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain.Entities;

namespace BookShop.Domain.Abstract
{
    public interface IBookRepository
    {
        IQueryable<Book> Books { get; }

        IEnumerable<Order> Orders { get; }

        IEnumerable<Publisher> Publishers { get; }

        IEnumerable<Author> Authors { get; }
        

        void SaveBook(Book book);

        Book DeleteBook(int  bookID);

        void SaveOrder(Order order);

        void SavePublisher(Publisher publisher);

        void SaveAuthor(Author author);


    }

    
}
