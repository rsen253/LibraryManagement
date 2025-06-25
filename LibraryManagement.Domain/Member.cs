using System.Collections.Generic;

namespace LibraryManagement.Domain
{
    public class Member
    {
        public int MemberId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Book> Books { get; set; }
    }

}
