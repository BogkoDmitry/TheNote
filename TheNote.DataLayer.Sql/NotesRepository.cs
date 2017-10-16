using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;
using System.Data;
using System.Data.SqlClient;

namespace TheNote.DataLayer.Sql
{
    public class NotesRepository : INotesRepository
    {
        private readonly string _connectionString;
        public NotesRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public Note Create(Note note)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    note.ID = Guid.NewGuid();
                    command.CommandText = "insert into notes (id, title, textcontent, createdate, changedate) " +
                        "values (@id, @title, @textcontent, @createdate, @updatedate)";
                    command.Parameters.AddWithValue("@id", note.ID);
                    command.Parameters.AddWithValue("@title", note.Title);
                    command.Parameters.AddWithValue("@textcontent", note.TextContent);
                    command.Parameters.AddWithValue("@createdate", note.Create.GetDateTimeFormats());
                    command.Parameters.AddWithValue("@changedate", note.Change.GetDateTimeFormats());
                    command.ExecuteNonQuery();

                    return note;
                }
            }
        }
        public void Delete(Guid ID)
        {
             using (var sqlConnection = new SqlConnection(_connectionString))
             {
                 sqlConnection.Open();
                 using (var command = sqlConnection.CreateCommand())
                 {
                        command.CommandText = "delete from notes where id = @id";
                        command.Parameters.AddWithValue("@id", ID);
                        command.ExecuteNonQuery();
                 }
             }
        }

        public Note Get(Guid id)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select id, title from note where id = @id";
                    command.Parameters.AddWithValue("@id", id);

                    using (var reader = command.ExecuteReader())
                    {
                        if (!reader.Read())
                            throw new ArgumentException($"Заметка с id {id} не найден");

                        var note = new Note
                        {
                            ID = reader.GetGuid(reader.GetOrdinal("id")),
                            Title = reader.GetString(reader.GetOrdinal("title"))
                        };

                        return note;
                    }
                }
            }
        }
    }
}
