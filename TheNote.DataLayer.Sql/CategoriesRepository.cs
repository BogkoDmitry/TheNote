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

        public Category Create(Guid UserID, string name)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {

                    command.CommandText = "insert into categories (id, name, userid) values (@id, @name, @userid)";
                    var category = new Category
                    {
                        ID = Guid.NewGuid(),
                        Name = name
                    };

                    command.Parameters.AddWithValue("@id", category.ID);                    
                    command.Parameters.AddWithValue("@name", category.Name);
                    command.Parameters.AddWithValue("@userid", UserID);
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

        public IEnumerable<Category> GetUsersCategories(Guid userID)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select id, name from categories where userid = @userid";
                    command.Parameters.AddWithValue("@userid", userID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Category
                            {
                                Name = reader.GetString(reader.GetOrdinal("name")),
                                ID = reader.GetGuid(reader.GetOrdinal("id"))
                            };
                        }
                    }
                }
            }
        }
    }
}
