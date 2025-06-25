﻿using System.Collections.Generic;

namespace LibraryManagement.Domain
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int LibraryId { get; set; }
        public virtual Library Library { get; set; }

        public virtual ICollection<Member> Members { get; set; }
    }

}
