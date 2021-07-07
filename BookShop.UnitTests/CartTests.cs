using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using BookShop.Domain.Entities;
using BookShop.Domain.Abstract;
using Moq;
using BookShop.WebUI.Controllers;

namespace BookShop.UnitTests
{
    [TestClass]
    public class CartTests
    {
        [TestMethod]
        public void Can_Add_New_lines()
        {

            Book b1 = new Book { BookID = 1, Title = "B1" };
            Book b2 = new Book { BookID = 2, Title = "B2" };

            Cart target = new Cart();

            target.AddItem(b1,1);
            target.AddItem(b2,1);

            CartLine[] results = target.Lines.ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Book, b1);
            Assert.AreEqual(results[1].Book, b2);
        }


        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            Book b1 = new Book { BookID = 1, Title = "B1" };
            Book b2 = new Book { BookID = 2, Title = "B2" };

            Cart target = new Cart();
            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b1, 4);

            CartLine[] results = target.Lines.OrderBy(c => c.Book.BookID).ToArray();

            Assert.AreEqual(results.Length, 2);
            Assert.AreEqual(results[0].Quantity, 5);
            Assert.AreEqual(results[1].Quantity, 1);
        }

        [TestMethod]
        public void Can_Remove_From_Cart()
        {
            Book b1 = new Book { BookID = 1, Title = "B1" };
            Book b2 = new Book { BookID = 2, Title = "B2" };
            Book b3 = new Book { BookID = 3, Title = "B3" };

            Cart target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b3, 2);
            target.AddItem(b1, 2);

            target.RemoveLine(b2);

            Assert.AreEqual(target.Lines.Where(c => c.Book == b2).Count(), 0);
            Assert.AreEqual(target.Lines.Count(), 2);
        }

        [TestMethod]
        public void Calculate_Total_Value()
        {
            Book b1 = new Book { BookID = 1, Title = "B1",Price=40M};
            Book b2 = new Book { BookID = 2, Title = "B2",Price=49.23M};

            Cart target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b1, 2);
            decimal result = target.ComputeTotalValue();

            Assert.AreEqual(result, 169.23M);
        }

        [TestMethod]
        public void Can_Clear_Cart()
        {
            Book b1 = new Book { BookID = 1, Title = "B1", Price = 40M };
            Book b2 = new Book { BookID = 2, Title = "B2", Price = 49.23M };

            Cart target = new Cart();

            target.AddItem(b1, 1);
            target.AddItem(b2, 1);
            target.AddItem(b1, 2);
            target.Clear();

            Assert.AreEqual(target.Lines.Count(),0);
        }


        [TestMethod]
        public void Can_Add_To_Cart()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book{BookID=1,Title="P1",Genre="G1"}, }.AsQueryable());

            Cart cart = new Cart();

            CartController target =new CartController(mock.Object);

            target.AddToCart(cart, 1, null);

            Assert.AreEqual(cart.Lines.Count(), 1);

        }


    }
}
