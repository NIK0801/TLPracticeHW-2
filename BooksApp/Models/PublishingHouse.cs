using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Models
{
    public class PublishingHouse
    {
        public int UUID { get; private set; }
        public string PublishingHouseName { get; private set; }
        public string Address { get; private set; }

        public PublishingHouse(int uUID, string publishingHouseName, string address)
        {
            UUID = uUID;
            PublishingHouseName = publishingHouseName;
            Address = address;
        }
        public void UpdatePublishingHouseName( string newPublishingHouseName)
        {
            PublishingHouseName = newPublishingHouseName;
        }
    }
}
