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

        public Note Create(Guid UserID, string title)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    var note = new Note
                    {
                        ID = Guid.NewGuid(),
                        Title = title
                    };

                    
                    command.CommandText = "insert into notes (id, title, textcontent, createdate, changedate, userid) " +
                        "values (@id, @title, @textcontent, @createdate, @updatedate, @userid)";
                    command.Parameters.AddWithValue("@id", note.ID);
                    command.Parameters.AddWithValue("@title", note.Title);
                    command.Parameters.AddWithValue("@textcontent", note.TextContent);
                    command.Parameters.AddWithValue("@createdate", note.Create.GetDateTimeFormats());
                    command.Parameters.AddWithValue("@changedate", note.Update.GetDateTimeFormats());
                    command.Parameters.AddWithValue("@userid", UserID);
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

        public IEnumerable<Note> GetUserNotes(Guid UserID)
        {
            using (var sqlConnection = new SqlConnection(_connectionString))
            {
                sqlConnection.Open();
                using (var command = sqlConnection.CreateCommand())
                {
                    command.CommandText = "select id, title, textcontent, createdate, updatedate from notes where userid = @userid";
                    command.Parameters.AddWithValue("@userid", UserID);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            yield return new Note
                            {
                                ID = reader.GetGuid(reader.GetOrdinal("id")),
                                Title = reader.GetString(reader.GetOrdinal("title"))
                            }; 
                        }
                    }
                }
            }
        }
    }
}
