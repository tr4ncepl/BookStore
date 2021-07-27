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

        IEnumerable<Genre> Genres { get; }

        IEnumerable<BookOrder> BookOrders { get; }

        
        

        void SaveBook(Book book,int publisherId, int authorId, int genreId);

        Book DeleteBook(int  bookID);

        void SaveOrder(Order order);

        void SavePublisher(Publisher publisher);

        void SaveAuthor(Author author);

        Author DeleteAuthor(int authorId);

        Publisher DeletePublisher(int publisherId);

        void SaveGenre(Genre genre);

        Genre DeleteGenre(int genreId);

        Order DeleteOrder(int orderId);


    }

    
}
