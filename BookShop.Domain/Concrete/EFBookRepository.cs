using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Genre> Genres
        {
            get { return context.Genres; }
        }

        public IEnumerable<BookOrder> BookOrders
        {
            get { return context.BookOrders; }
        }

        public IEnumerable<BookReview> BookReviews
        {
            get { return context.BookReviews; }
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

        public Order DeleteOrder(int orderId)
        {
            Order orderEntry = context.Orders.Find(orderId);
            if(orderEntry!=null)
            {
                context.Orders.Remove(orderEntry);
            }
            var bOrderEntry = context.BookOrders
                .Where(bo => bo.order.OrderId == orderId);

            foreach(var value in bOrderEntry)
            {
                if(value!=null)
                {
                    context.BookOrders.Remove(value);
                }
            }
            context.SaveChanges();

            return orderEntry;
        }

        public Author DeleteAuthor(int authorId)
        {
            Author dbEntry = context.Authors.Find(authorId);
            if(dbEntry!=null)
            {
                context.Authors.Remove(dbEntry);
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

        public Publisher DeletePublisher(int publisherId)
        {
            Publisher dbEntry = context.Publishers.Find(publisherId);
            if(dbEntry!=null)
            {
                context.Publishers.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
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
                    dbEntry.AuthorDesc = author.AuthorDesc;
                }
            }
            context.SaveChanges();
        }
        public void SaveBook(Book book,int publisherId, int authorId, int genreId)
        {
            var publisher = context.Publishers.FirstOrDefault(p => p.PublisherId == publisherId);
            book.Publisher = publisher;
            var author = context.Authors.FirstOrDefault(a => a.AuthorId == authorId);
            book.Author = author;
            var genre = context.Genres.FirstOrDefault(g => g.GenreId == genreId);
            book.Genre = genre;
            if (book.BookID ==0)
            { 
                context.Books.Add(book);
            }
            else
            {
                Book dbEntry = context.Books.Find(book.BookID);
                if(dbEntry!=null)
                {
                    dbEntry.PagesNumber = book.PagesNumber;
                    dbEntry.Title = book.Title;
                    dbEntry.Description = book.Description;
                    dbEntry.Price = book.Price;
                    dbEntry.Genre = book.Genre;
                    dbEntry.Rating = book.Rating;
                    dbEntry.ImageData = book.ImageData;
                    dbEntry.ImageMimeType = book.ImageMimeType;
                    dbEntry.Publisher = publisher;
                    dbEntry.Author = author;
                    dbEntry.Genre = genre;
                    
                }
            }

            context.SaveChanges();
        }


        public void SaveGenre(Genre genre)
        {
         if(genre.GenreId==0)
            {
                context.Genres.Add(genre);
            }
            else
            {
                Genre dbEntry = context.Genres.Find(genre.GenreId);
               {
                    dbEntry.GenreName = genre.GenreName;
 
                }
            }
            context.SaveChanges();
        }

        public void AddReview(BookReview review, int bookId)
        {
            var book = context.Books.Find(bookId);
            review.Book = book;
            context.BookReviews.Add(review);
            context.SaveChanges();
        }

        public void AddBookToOrder(BookOrder bookOrder)
        {
            var checkIfExist = context.BookOrders.Find(bookOrder.book.BookID, bookOrder.order.OrderId);

            if(checkIfExist!=null)
            {
                checkIfExist.Quantity += bookOrder.Quantity;
            }
            else
            {
                context.BookOrders.Add(bookOrder);
            }

            
            
            Order dbEntry = context.Orders.Find(bookOrder.order.OrderId);
            var query = context.BookOrders
                .Where(o => o.order.OrderId == bookOrder.order.OrderId);

            var value = query.Sum(e => e.book.Price * e.Quantity);
            if(dbEntry!=null)
            {
                dbEntry.TotalValue = value;
            }

            context.SaveChanges();
        }

        

        public Genre DeleteGenre(int genreId)
        {
            Genre dbEntry = context.Genres.Find(genreId);
            if(dbEntry!=null)
            {
                context.Genres.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public BookOrder DeleteBookInOrder(int orderId, int bookId)
        {
            BookOrder dbEntry = context.BookOrders
                .Where(bo => bo.order.OrderId == orderId && bo.book.BookID == bookId).First();
            if(dbEntry!=null)
            {
                context.BookOrders.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }
    }
}
