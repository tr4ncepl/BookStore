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

        public IEnumerable<Author> Authors
        {
            get { return context.Authors; }
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
                Publisher dbEntry = context.Publishers.Find(publisher.PublisherId);
                if(dbEntry!=null)
                {
                    dbEntry.PublisherName = publisher.PublisherName;
                    dbEntry.PublisherDesc = publisher.PublisherDesc;
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

        public void SaveAuthor(Author author)
        {
            if(author.AuthorId==0)
            {
                context.Authors.Add(author);
            }
            else
            {
                Author dbEntry = context.Authors.Find(author.AuthorId);
                if(dbEntry!=null)
                {
                    dbEntry.AuthorName = author.AuthorName;
                    dbEntry.AuthorLastName = author.AuthorLastName;
                }
            }
            context.SaveChanges();
        }
        public void SaveBook(Book book,int publisherId, int authorId)
        {
            var publisher = context.Publishers.FirstOrDefault(p => p.PublisherId == publisherId);
            book.Publisher = publisher;
            var author = context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            book.Author = author;
            if (book.BookID ==0)
            { 
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);
                if(dbEntry!=null)
                {
                    dbEntry.Title = book.Title;
                    dbEntry.Description = book.Description;
                    dbEntry.Price = book.Price;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Rating = book.Rating;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                    dbEntry.Publisher = publisher;
                    dbEntry.Author = author;
                    
                }
            }

            context.SaveChanges();
        }
    }
}
