using IyiOlusAdminPanel.Models.Questions;

namespace IyiOlusAdminPanel.Models.Contacts
{
    public class ContactResponse
    {
        public List<Contact> Items { get; set; } = default!;
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int totalItems { get; set; }
        public bool hasPreviousPage { get; set; }
        public bool hasNextPage { get; set; }
    }
}
