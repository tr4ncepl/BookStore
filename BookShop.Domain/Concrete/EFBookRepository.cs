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

        public IEnumerable<Book> Books
        {
            get { return context.Books; }
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
