using System;
using System.Collections.Generic;

#nullable disable

namespace DemoApi
{
    public partial class Book
    {
        public Book()
        {
            BookAuthors = new HashSet<BookAuthor>();
        }

        public long Id { get; set; }
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public long PublisherId { get; set; }

        public virtual BookCategory Category { get; set; }
        public virtual Publisher Publisher { get; set; }
        public virtual ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
