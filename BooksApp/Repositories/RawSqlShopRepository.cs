using System.Data;
using System.Data.SqlClient;
using System.Xml.Linq;
using BooksApp.Models;

namespace BooksApp.Repositories
{
    public class RawSqlShopRepository : IShopRepository
    {
        private readonly string _connectionString;

        public RawSqlShopRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Shop GetByShopName(string shopName)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [UUID], [ShopName], [Address] from [Shop] where [ShopName] = @shopName";
            sqlCommand.Parameters.Add("@shopName", SqlDbType.NVarChar, 100).Value = shopName;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Shop(
                    Convert.ToInt32(reader["UUID"]),
                    Convert.ToString(reader["ShopName"]),
                    Convert.ToString(reader["Address"]));
            }
            else
            {
                return null;
            }
        }

        public Shop GetById(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "select [UUID], [ShopName], [Address] from [Shop] where [UUID] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = id;

            using SqlDataReader reader = sqlCommand.ExecuteReader();
            if (reader.Read())
            {
                return new Shop(
                    Convert.ToInt32(reader["UUID"]),
                    Convert.ToString(reader["ShopName"]),
                    Convert.ToString(reader["Address"]));
            }
            else
            {
                return null;
            }
        }

        public void Update(Shop shop)
        {
            if (shop == null)
            {
                throw new ArgumentNullException(nameof(shop));
            }

            using var connection = new SqlConnection(_connectionString);
            connection.Open();

            using SqlCommand sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = "update [Shop] set [ShopName] = @name, [Address] = @address where [UUID] = @id";
            sqlCommand.Parameters.Add("@id", SqlDbType.Int).Value = shop.UUID;
            sqlCommand.Parameters.Add("@name", SqlDbType.NVarChar, 100).Value = shop.ShopName;
            sqlCommand.Parameters.Add("@address", SqlDbType.NVarChar, 100).Value = shop.Address;
            sqlCommand.ExecuteNonQuery();
        }
    }
}
