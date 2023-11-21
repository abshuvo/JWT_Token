using System;
using System.Collections.Generic;

#nullable disable

namespace DemoApi
{
    public partial class BookAuthor
    {
        public long BookId { get; set; }
        public long AuthorId { get; set; }

        public virtual Author Author { get; set; }
        public virtual Book Book { get; set; }
    }
}
