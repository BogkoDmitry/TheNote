using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;
using System.Data.SqlClient;
using System.Data;

namespace TheNote.DataLayer.Sql
{
    public class CategoriesRepository: ICategoriesRepository
    {
        private readonly string _connectionString;

        public CategoriesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Category Create(Category category)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    category.ID = Guid.NewGuid();
                    command.CommandText = "insert into categories (id, name, description) values (@id, @name, @description)";
                    command.Parameters.AddWithValue("@id", category.ID);
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@description", category.Description);
                    command.ExecuteNonQuery();

                    return category;
                }
            }
        }

        public void Delete(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "delete from categories where id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public Category Get(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select id, name from categories where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Категория с id {id} не найден");

                        var category = new Category
                        {
                            ID = reader.GetGuid(reader.GetOrdinal("id")),
                            Name = reader.GetString(reader.GetOrdinal("name"))
                        };

                        return category;
                    }
                }
            }
        }
    }
}
