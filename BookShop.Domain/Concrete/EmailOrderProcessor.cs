using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using BookShop.Domain.Abstract;
using BookShop.Domain.Entities;
using System.Net;
using System.Web.Razor.Parser.SyntaxTree;

namespace BookShop.Domain.Concrete
{

    public class EmailSettings
    {
        public string MailToAddress = "zamowienialykeion@gmail.com";
        public string MailFromAddress = "ksiegarnialykeion@gmail.com";
        public bool UseSsl = true;
        public string Username = "Test";
        public string Password = "Lykeion123$";
        public string ServerName = "smtp.gmail.com";
        public int ServerPort = 587;
        public bool WriteAsFile = false;
        public string FileLocation = @"c:\book_shop_emails";
    }


    
    public class EmailOrderProcessor : IOrderProcessor
    {

        

       

        private EmailSettings emailSettings;

        public EmailOrderProcessor(EmailSettings settings)
        {
            emailSettings = settings;
        }

        public void ProcessOrder(Cart cart, ShippingDetails shippingInfo)
        {
           
            var fromAddress = new MailAddress("ksiegarnialykeion@gmail.com", "Zamowienie");
            var toAddress = new MailAddress("zamowienialykeion@gmail.com", "To Name");
            const string fromPassword = "Lykeion123$";
            const string subject = "Nowe zamówienie";

                StringBuilder body = new StringBuilder()
                    .AppendLine("Nowe zamówienie")
                    .AppendLine("---")
                    .AppendLine("Zawartość zamówienia:");

                foreach(var line in cart.Lines)
                {
                    var subtotal = line.Book.Price * line.Quantity;
                    body.AppendFormat($"{line.Quantity} x {line.Book.Title} : {subtotal:c}\n");

                }

                body.AppendLine("")
                    .AppendFormat("Wartość całkowita: {0:c}\n", cart.ComputeTotalValue())
                    .AppendLine("----")
                    .AppendLine("Dane wysyłkowe:")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1 ?? "")
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.State ?? "")
                    .AppendLine(shippingInfo.Country)
                    .AppendLine(shippingInfo.Zip)
                    .AppendLine("----")
                    .AppendFormat("Pakowanie prezentu: {0}", shippingInfo.GiftWrap ? "Tak" : "Nie");



            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000
            };

            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body.ToString()
            })
            {
                smtp.Send(message);
            }

            
                var newOrder1 = new Order
                {
                    RecAdress = shippingInfo.Line1,
                    RecCity = shippingInfo.City,
                    RecCountry = shippingInfo.Country,
                    RecName = shippingInfo.Name,
                    RecZip = shippingInfo.Zip,
                    RecState = shippingInfo.State,
                    GiftWrap = shippingInfo.GiftWrap,
                    TotalValue = cart.Lines.Sum(e => e.Book.Price * e.Quantity)
                };

                CartLine[] numBook = cart.Lines.ToArray();



            using (var context = new EFDbContext())
            {
                int n = numBook[0].Book.BookID;
                var book = context.Books
                            .FirstOrDefault(b => b.BookID == n);
                var bookOrders = new BookOrder { order = newOrder1, book = book, Quantity = 2 };
                //context.Orders.Add(newOrder1);
                context.BookOrders.Add(bookOrders);
                context.SaveChanges();
            }
            
                
                
                
            



        }
    }
}
