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
        Category Create(Category category);
        void Delete(Guid ID);
        Category Get(Category category);

    }
}
