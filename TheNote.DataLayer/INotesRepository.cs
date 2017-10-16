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
        Note Create(Note note);
        void Delete(Guid ID);
        Note Get(Guid ID);
    }
}
