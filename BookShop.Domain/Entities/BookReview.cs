using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShop.Domain.Entities
{
    public class BookReview
    {
        public int BookReviewId { get; set; }

        public string ReviewDesc { get; set; }

        public string ReviewAuthor { get; set; }

        [DataType(DataType.Date)]
        public DateTime PublicationDate { get; set; }

        public Book Book { get; set; }

        public int BookRating { get; set; }


    }
}
