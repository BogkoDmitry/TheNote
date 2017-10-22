using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;

namespace TheNote.DataLayer
{
    public interface INotesRepository
    {
        Note Create(Guid userID, string title);

        void Delete(Guid ID);

        IEnumerable<Note> GetUserNotes(Guid UserID);
        
    }
}
