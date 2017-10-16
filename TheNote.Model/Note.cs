using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheNote.Model
{
    public class Note
    {
        public Guid ID { get; set; }

        public string Title { get; set; }

        public string TextContent { get; set; }

        public DateTime Create { get; set; }

        public DateTime Change { get; set; }

        public User Owner { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public IEnumerable<User> Shared { get; set; }
    }
}
