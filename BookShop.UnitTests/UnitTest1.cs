using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;
using BookShop.WebUI.Controllers;
using BookShop.WebUI.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;


namespace BookShop.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Generate_Category_Specific_Book_Count()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            mock.Setup(m => m.Books).Returns(new Book[]
            {
                new Book{BookID =1, Title ="B1",Author="A1",Genre="G1"},
                new Book{BookID =2, Title ="B2",Author="A2",Genre="G2"},
                new Book{BookID =3, Title ="B3",Author="A3",Genre="G1"},
                new Book{BookID =4, Title ="B4",Author="A4",Genre="G3"},
                new Book{BookID =5, Title ="B5",Author="A5",Genre="G3"},
                new Book{BookID =6, Title ="B6",Author="A2",Genre="G1"},
                new Book{BookID =7, Title ="B7",Author="A3",Genre="G2"},
                new Book{BookID =8, Title ="B8",Author="A2",Genre="G1"},

            });

            BookController target = new BookController(mock.Object);

            target.PageSize = 3;

            int res1 = ((BookListViewModel)target.List("G1").Model).PagingInfo.TotalItems;
            int res2 = ((BookListViewModel)target.List("G2").Model).PagingInfo.TotalItems;
            int res3 = ((BookListViewModel)target.List("G3").Model).PagingInfo.TotalItems;
            int resALL =  ((BookListViewModel)target.List(null).Model).PagingInfo.TotalItems;

            Assert.AreEqual(res1, 4);
            Assert.AreEqual(res2, 2);
            Assert.AreEqual(res3, 2);
            Assert.AreEqual(resALL, 8);

        }
        
    }
}
