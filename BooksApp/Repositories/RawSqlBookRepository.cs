using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using BooksApp.Models;

namespace BooksApp.Repositories
{
    public class RawSqlBookRepository : IBookRepository
    {
        private readonly string _connectionString;

        public RawSqlBookRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IReadOnlyList<Book> GetAll()
        {
            var result = new List<Book>();

            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [UUID], [BookName], [Author], [ShopId], [PublishingHouseId], [NumberOfPages] from [Book]";

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            while( reader.Read() )
            {
                result.Add(new Book(
                    Convert.ToInt32(reader["UUID"]),
                    Convert.ToString(reader["BookName"]),
                    Convert.ToString(reader["Author"]),
                    Convert.ToInt32(reader["ShopId"]),
                    Convert.ToInt32(reader["PublishingHouseId"]),
                    Convert.ToInt32(reader["NumberOfPages"])
                    ) );
            }

            return result;
        }

        public IReadOnlyList<Book> GetByShop(int id)
        {
            var result = new List<Book>();

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [BookName], count(BookName) as Amount from [Book] where [ShopId] = @id group by [BookName]";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            using SqlDataReader reader = sqlCommand.ExecuteReader();
            
            while (reader.Read())
            {
                result.Add(new Book(
                    Convert.ToString(reader["BookName"])
                    ));
            }

            return result;
        }

        public Book GetByBookName( string bookName)
        {
            using var connection = new SqlConnection( _connectionString );
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [UUID], [BookName], [Author], [ShopId], [PublishingHouseId], [NumberOfPages] from [Book] where [BookName] = @bookName";
            sqlCommand.Parameters.Add( "@bookName", SqlDbType.NVarChar, 100).Value = bookName;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if ( reader.Read() )
            {
                return new Book(
                    Convert.ToInt32(reader["UUID"]),
                    Convert.ToString(reader["BookName"]),
                    Convert.ToString(reader["Author"]),
                    Convert.ToInt32(reader["ShopId"]),
                    Convert.ToInt32(reader["PublishingHouseId"]),
                    Convert.ToInt32(reader["NumberOfPages"]));
            }
            else
            {
                return null;
            }
        }

        public Book GetById( int id )
        {
            using var connection = new SqlConnection( _connectionString ) ;
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand() ;
            sqlCommand.CommandText = "select [UUID], [BookName], [Author], [ShopId], [PublishingHouseId], [NumberOfPages] from [Book] where [UUID] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Book(
                    Convert.ToInt32(reader["UUID"]),
                    Convert.ToString(reader["BookName"]),
                    Convert.ToString(reader["Author"]),
                    Convert.ToInt32(reader["ShopId"]),
                    Convert.ToInt32(reader["PublishingHouseId"]),
                    Convert.ToInt32(reader["NumberOfPages"]));
            }
            else
            {
                return null;
            }
        }


        public void Delete( Book book)
        {
            if ( book == null )
            {
                throw new ArgumentNullException(nameof(book));
            }

            using var connection = new SqlConnection( _connectionString ) ;
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "delete [Book] where [UUID] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = book.UUID;
            sqlCommand.ExecuteNonQuery();
        }

        public void Update( Book book )
        {
            if ( book == null )
            {
                throw new ArgumentNullException(nameof(book));
            }

            using var connection = new SqlConnection(_connectionString ) ;
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Book] set [BookName] = @name where [UUID] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = book.UUID;
            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = book.BookName;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
