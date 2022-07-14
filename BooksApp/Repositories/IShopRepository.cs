using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksApp.Models;
namespace BooksApp.Repositories
{
    public interface IShopRepository
    {
        Shop GetByShopName(string shopName);
        Shop GetById(int id);
        void Update(Shop shop);
        
    }
}
