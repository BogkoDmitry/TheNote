using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;

namespace TheNote.DataLayer
{
    public interface IUsersRepository
    {
        User Create (User user);

        void Delete(Guid ID);

        User Get(Guid ID);
    }
}
