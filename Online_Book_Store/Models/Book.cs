using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Online_Book_Store.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Bookname { set; get; }
        public string Authorname { set; get; }
        public string BookType { set; get; }
        public int price { set; get; }
    }
}