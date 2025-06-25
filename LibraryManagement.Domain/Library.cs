using System.Collections.Generic;

namespace LibraryManagement.Domain
{
    public class Library
    {
        public int LibraryId { get; set; }
        public string Name { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }
        public virtual List<Book> Books { get; set; }
    }

}
