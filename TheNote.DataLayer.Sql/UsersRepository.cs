using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;
using System.Data.SqlClient;

namespace TheNote.DataLayer.Sql
{
    public class UsersRepository: IUsersRepository
    {
        private readonly string _connectionString;
        private readonly ICategoriesRepository _categoriesRepository;

        public UsersRepository(string connectionString, ICategoriesRepository categoriesRepository)
        {
            _connectionString = connectionString;
        }

        public User Create(User user)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    user.ID = Guid.NewGuid();
                    command.CommandText = "insert into users (id, firstname, lastname) values (@id, @firstname, @lastname)";
                    command.Parameters.AddWithValue("@id", user.ID);
                    command.Parameters.AddWithValue("@firstname", user.FirstName);
                    command.Parameters.AddWithValue("@lastname", user.LastName);
                    command.ExecuteNonQuery();

                    return user;
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
                    command.CommandText = "delete from users where id = @id";
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public User Get(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select id, name from users where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Пользователь с id {id} не найден");

                        var user = new User
                        {
                            ID = reader.GetGuid(reader.GetOrdinal("id")),
                            FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                            LastName = reader.GetString(reader.GetOrdinal("lastname"))
                        };
                        
                        return user;
                    }
                }
            }
        }
    }
}
