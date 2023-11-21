using System;
using System.Collections.Generic;

#nullable disable

namespace DemoApi
{
    public partial class Author
    {
        public Author()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        public virtual AuthorContact AuthorContact { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
