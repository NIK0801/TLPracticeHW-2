using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Models
{
    public class Book
    {
        public int UUID { get; private set; }
        public string BookName { get; private set; }
        public string Author { get; private set; }
        public int ShopId { get; private set; }
        public int PublishingHouseId { get; private set; }
        public int NumberOfPages { get; private set; }

        public Book(int uUID, string bookName, string author, int shopId, int publishingHouseId, int numberOfPages)
        {
            UUID = uUID;
            BookName = bookName;
            Author = author;
            ShopId = shopId;
            PublishingHouseId = publishingHouseId;
            NumberOfPages = numberOfPages;
        }


        public Book(string bookName)
        {
            BookName = bookName;
        }
        public void UpdateBookName( string newBookName )
        {
            BookName = newBookName;
        }
    }
}
