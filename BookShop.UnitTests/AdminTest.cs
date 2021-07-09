using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;
using BookShop.WebUI.Controllers;

namespace BookShop.UnitTests
{
    [TestClass]
    public class AdminTest
    {
        [TestMethod]
        public void Index_Contains_All_Products()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
                {
                new Book { BookID =1, Title = "T1"},
                new Book {  BookID=2, Title="T2"},
                new Book {  BookID =3, Title="T3"}
            });


            AdminController target = new AdminController(mock.Object);

            Book[] result = ((IEnumerable<Book>)target.Index().
                ViewData.Model).ToArray();

            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("T1", result[0].Title);
            Assert.AreEqual("T2", result[1].Title);
            Assert.AreEqual("T3", result[2].Title);

        }


        [TestMethod]
        public void Can_Edit_Book()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book{BookID =1, Title="T1"},
                new Book{BookID=2,Title="T2"},
                new Book{BookID=3,Title="T3"}
            });


            AdminController target = new AdminController(mock.Object);

            Book p1 = target.Edit(1).ViewData.Model as Book;

        }

        [TestMethod]
        public void Can_Delete_Book()
        {
            Book book1 = new Book { BookID = 2, Title = "T1" };
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book{BookID =1, Title="T2"},
                book1,
                new Book{BookID = 3,Title="t#"}
            }) ;

            AdminController target = new AdminController(mock.Object);

            target.Delete(book1.BookID);


            mock.Verify(m => m.DeleteBook(book1.BookID));

        }
    }


    
}
