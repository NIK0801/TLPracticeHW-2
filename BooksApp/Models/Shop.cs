using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Models
{
    public class Shop
    {
        public int UUID { get; private set; }
        public string ShopName { get; private set; }
        public string Address { get; private set; }

        public Shop(int uUID, string shopName, string address)
        {
            UUID = uUID;
            ShopName = shopName;
            Address = address;
        }

        public void UpdateShop( string newShopName, string newAddress)
        {
            ShopName = newShopName;
            Address = newAddress;
        }
    }
}
