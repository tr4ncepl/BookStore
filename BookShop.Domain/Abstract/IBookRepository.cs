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
        IEnumerable<Book> Books { get; }


        IEnumerable<Order> Orders { get; }
        

        void SaveBook(Book book);

        Book DeleteBook(int  bookID);

        void SaveOrder(Order order);
    }

    
}
