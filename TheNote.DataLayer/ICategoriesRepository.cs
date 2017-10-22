using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheNote.Model;


namespace TheNote.DataLayer
{
    public interface ICategoriesRepository
    {
        Category Create(Guid UserID, string name);

        void Delete(Guid ID);

        IEnumerable<Category> GetUsersCategories(Guid UserID);

    }
}
