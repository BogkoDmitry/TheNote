using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheNote.DataLayer;
using TheNote.DataLayer.Sql;
using TheNote.Model;

namespace TheNote.Api.Controllers
{
    public class NotesController : ApiController
    {
        private const string ConnectionString = @"Data Source=AGATA\SQLEXPRESS;Initial Catalog=db; Integrated Security=True;";
        private readonly INotesRepository _notesRepository;

        public  NotesController()
        {
            _notesRepository = new NotesRepository(ConnectionString);
        }

        [HttpPost]
        [Route("api/notes")]

        public Note Create([FromBody] User user, string title)
        {
            return _notesRepository.Create(user.ID, title);
        }

        [HttpDelete]
        [Route("api/notes/{id}")]

        public void Delete(Guid id)
        {
            _notesRepository.Delete(id);
        }

        [HttpGet]
        [Route("api/user/{id}/notes")]

        public IEnumerable<Note> GetUsersNotes(Guid id)
        {
            return _notesRepository.GetUserNotes(id);
        }
    }
}
