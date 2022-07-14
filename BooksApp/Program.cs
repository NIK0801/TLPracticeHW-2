using BooksApp.Models;
using BooksApp.Repositories;
using System.Data.SqlClient;

const string connectionString = @"Data Source=DESKTOP-LQ9J3MG\SQLEXPRESS;Initial Catalog=books;Pooling=true;Integrated Security=SSPI;TrustServerCertificate=True";

IBookRepository bookRepository = new RawSqlBookRepository(connectionString);
IShopRepository shopRepository = new RawSqlShopRepository(connectionString);
PrintCommands();

while ( true )
{
    Console.WriteLine("Введите команду:");
    string command = Console.ReadLine();

    if ( command == "get-books" )
    {
        IReadOnlyList<Book> books = bookRepository.GetAll();
        if ( books.Count == 0 )
        {
            Console.WriteLine( "Книги не найдены!" );
            continue;
        }

        foreach ( Book book in books)
        {
            Console.WriteLine($"Id: {book.UUID}, BookName: {book.BookName}, " +
                $"Author: {book.Author}, ShopId: {book.ShopId}, PublishingHouseId: {book.PublishingHouseId}, " +
                $"NumberOfPages: {book.NumberOfPages}");
        }
    }
    if (command == "get-books-by-shop")
    {
        try
        {
            Console.WriteLine("Введите Id магазина: ");
            int id = int.Parse(Console.ReadLine());
            Shop shop = shopRepository.GetById(id);
            IReadOnlyList<Book> books = bookRepository.GetByShop(id);
            if (books.Count == 0)
            {
                Console.WriteLine("Книги не найдены!");
                continue;
            }
            Console.WriteLine($"Магазин:{shop.ShopName}");
            foreach (Book book in books)
            {
                Console.WriteLine($"BookName: {book.BookName}");
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Id - это число!");
        }
        
    }
    else if ( command == "get-by-bookName")
    {
        try
        {
            Console.WriteLine("Введите название книги: ");
            string bookName = Console.ReadLine();
            Book book = bookRepository.GetByBookName(bookName);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена!");
            }
            else
            {
                Console.WriteLine($"Id: {book.UUID}, BookName: {book.BookName}, " +
                    $"Author: {book.Author}, ShopId: {book.ShopId}, PublishingHouseId: {book.PublishingHouseId}, " +
                    $"NumberOfPages: {book.NumberOfPages}");
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Название книги - это строка!");
        }
        
    }
    else if (command == "get-by-shopName")
    {
        try
        {
            Console.WriteLine("Введите название магазина: ");
            string shopName = Console.ReadLine();
            Shop shop = shopRepository.GetByShopName(shopName);
            if (shop == null)
            {
                Console.WriteLine("Магазин не найден!");
            }
            else
            {
                Console.WriteLine($"Id: {shop.UUID}, ShopName: {shop.ShopName}, " +
                    $"Address: {shop.Address}");
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Название магазина - это строка!");
        }
        
    }
    else if ( command == "delete-book-by-bookName")
    {
        try
        {
            Console.WriteLine("Введите название книги: ");
            string bookName = Console.ReadLine();
            Book book = bookRepository.GetByBookName(bookName);
            if (bookName == null)
            {
                Console.WriteLine("Книга не найдена");
            }
            else
            {
                bookRepository.Delete(book);
                Console.WriteLine("Книга удалена");
            }
        }
        catch(FormatException)
        {
            Console.WriteLine("Название книги - это строка!");
        }
        
    }
    else if ( command == "update-bookName")
    {

        try
        {
            Console.WriteLine("Введите Id: ");
            int id = int.Parse(Console.ReadLine());
            Book book = bookRepository.GetById(id);
            if (book == null)
            {
                Console.WriteLine("Книга не найдена");
                continue;
            }
            Console.WriteLine("Введите обновленное название: ");
            string newBookName = Console.ReadLine();
            book.UpdateBookName(newBookName);

            bookRepository.Update(book);
            Console.WriteLine("Название обновлено");
        }
        catch(FormatException)
        {
            Console.WriteLine("Id - это число!");
        }
            
    }
    else if (command == "update-Shop")
    {
        try
        {
            Console.WriteLine("Введите Id: ");
            int id = int.Parse(Console.ReadLine());
            Shop shop = shopRepository.GetById(id);
            if (shop == null)
            {
                Console.WriteLine("Книга не найдена");
                continue;
            }
            Console.WriteLine("Введите обновленное название магазина: ");
            string newShopName = Console.ReadLine();
            Console.WriteLine("Введите обновленный адрес: ");
            string newShopAddress = Console.ReadLine();
            shop.UpdateShop(newShopName, newShopAddress);

            shopRepository.Update(shop);
            Console.WriteLine("Магазин обновлен");
        }
        catch (FormatException)
        {
            Console.WriteLine("Id - это число!");
        }
        
    }
    else if ( command == "help" )
    {
        PrintCommands();
    }
    else if ( command == "exit" )
    {
        break;
    }
    else
    {
        Console.WriteLine("Неправильно введенная команда");
    }
}

void PrintCommands()
{
    Console.WriteLine("Доступные команды:");
    Console.WriteLine("get-books - Получить список всех книг");
    Console.WriteLine("get-books-by-shop - Получить список всех книг в магазине");
    Console.WriteLine("get-by-bookName - Получить книгу по названию");
    Console.WriteLine("get-by-shopName - Получить магазин по названию(из другой таблицы)");
    Console.WriteLine("delete-book-by-bookName - Удалить книгу по названию");
    Console.WriteLine("update-bookName - Обновить название книги");
    Console.WriteLine("update-Shop - Обновить магазин");
    Console.WriteLine("help - Список команд");
    Console.WriteLine("exit - Выход");
}


