namespace LibraryManagement.Domain
{
    public class Address
    {
        public int AddressId { get; set; }
        public string Location { get; set; }

        public virtual Library Library { get; set; }
    }

}
