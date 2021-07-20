using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;


namespace BookShop.Domain.Concrete
{
    public class EFBookRepository : IBookRepository
    {
        private EFDbContext context = new EFDbContext();

        public IQueryable<Book> Books
        {
            get { return context.Books; }
        }

        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }

        public IEnumerable<Publisher> Publishers
        {
            get { return context.Publishers; }
        }


        public Book DeleteBook(int bookID)
        {
            Book dbEntry = context.Books.Find(bookID);
            if(dbEntry!=null)
            {
                context.Books.Remove(dbEntry);
                context.SaveChanges();
            }

            return dbEntry;

        }

        public void SavePublisher(Publisher publisher)
        {
            if(publisher.PublisherId==0)
            {
                context.Publishers.Add(publisher);
            }
            else
            {
                Publisher dbentry = context.Publishers.Find(publisher.PublisherId);
                if(dbentry!=null)
                {
                    dbentry.PublisherName = publisher.PublisherName;
                    dbentry.PublisherDescription = publisher.PublisherDescription;
                }
            }

            context.SaveChanges();
        }

        public void SaveOrder(Order order)
        {
            if(order.OrderId==0)
            {
                context.Orders.Add(order);
            }
            else
            {
                Order dbEntry = context.Orders.Find(order.OrderId);
                if(dbEntry!=null)
                {
                    dbEntry.RecName = order.RecName;
                    dbEntry.RecAdress = order.RecAdress;
                    dbEntry.RecCity = order.RecCity;
                    dbEntry.RecCountry = order.RecCountry;
                    dbEntry.RecState = order.RecState;
                    dbEntry.RecZip = order.RecZip;
                    dbEntry.TotalValue = order.TotalValue;
                    
                }
            }

            context.SaveChanges();
        }
        public void SaveBook(Book book)
        {
            if(book.BookID ==0)
            {
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);
                if(dbEntry!=null)
                {
                    dbEntry.Title = book.Title;
                    dbEntry.Author = book.Author;
                    dbEntry.Description = book.Description;
                    dbEntry.Price = book.Price;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Rating = book.Rating;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                }
            }

            context.SaveChanges();
        }
    }
}
